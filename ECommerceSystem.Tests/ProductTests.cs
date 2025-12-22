using System;
using System.Collections.Generic;
using Xunit;
using ECommerceSystem.Models;

namespace ECommerceSystem.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_ValidData_CreatesSuccessfully()
        {
            Product.ClearExtent();
            var images = new List<string> { "image1.jpg", "image2.jpg" };
            var product = new Product("Laptop", "Gaming laptop", "SKU123",
                                     1299.99m, 50, 10, images);
            Assert.Equal("Laptop", product.Name);
            Assert.Equal("Gaming laptop", product.Description);
            Assert.Equal("SKU123", product.SKU);
            Assert.Equal(1299.99m, product.Price);
            Assert.Equal(50, product.StockQuantity);
            Assert.Equal(10, product.MinimumStock);
            Assert.Equal(2, product.Images.Count);
        }

        [Fact]
        public void Product_MoreThan5Images_ThrowsException()
        {
            Product.ClearExtent();
            var images = new List<string> { "img1.jpg", "img2.jpg", "img3.jpg",
                                           "img4.jpg", "img5.jpg", "img6.jpg" };
            Assert.Throws<ArgumentException>(() =>
                new Product("Product", "Description", "SKU123", 99.99m, 50, 10, images));
        }

        [Fact]
        public void Product_NoImages_ThrowsException()
        {
            Product.ClearExtent();
            var images = new List<string>();
            Assert.Throws<ArgumentException>(() =>
                new Product("Product", "Description", "SKU123", 99.99m, 50, 10, images));
        }

        [Fact]
        public void Product_CheckIfLowStock_ReturnsTrueWhenLow()
        {
            Product.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Product", "Description", "SKU123",
                                     99.99m, 5, 10, images);
            Assert.True(product.CheckIfLowStock());
        }

        [Fact]
        public void Product_CheckIfLowStock_ReturnsFalseWhenAdequate()
        {
            Product.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Product", "Description", "SKU123",
                                     99.99m, 50, 10, images);
            Assert.False(product.CheckIfLowStock());
        }

        [Fact]
        public void Product_AddImage_AddsImageSuccessfully()
        {
            Product.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Product", "Description", "SKU123",
                                     99.99m, 50, 10, images);

            product.AddImage("image2.jpg");
            Assert.Equal(2, product.Images.Count);
            Assert.Contains("image2.jpg", product.Images);
        }

        [Fact]
        public void Product_AddImage_MoreThan5Images_ThrowsException()
        {
            Product.ClearExtent();
            var images = new List<string> { "img1.jpg", "img2.jpg", "img3.jpg", "img4.jpg", "img5.jpg" };
            var product = new Product("Product", "Description", "SKU123",
                                     99.99m, 50, 10, images);

            Assert.Throws<InvalidOperationException>(() => product.AddImage("img6.jpg"));
        }

        [Fact]
        public void Product_RemoveImage_RemovesImageSuccessfully()
        {
            Product.ClearExtent();
            var images = new List<string> { "image1.jpg", "image2.jpg" };
            var product = new Product("Product", "Description", "SKU123",
                                     99.99m, 50, 10, images);

            product.RemoveImage("image1.jpg");
            Assert.Equal(1, product.Images.Count);
            Assert.DoesNotContain("image1.jpg", product.Images);
        }

        [Fact]
        public void Product_RemoveImage_LastImage_ThrowsException()
        {
            Product.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Product", "Description", "SKU123",
                                     99.99m, 50, 10, images);

            Assert.Throws<InvalidOperationException>(() => product.RemoveImage("image1.jpg"));
        }

        [Fact]
        public void Product_EmptyName_ThrowsException()
        {
            Product.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            Assert.Throws<ArgumentException>(() =>
                new Product("", "Description", "SKU123", 99.99m, 50, 10, images));
        }

        [Fact]
        public void Product_ZeroPrice_ThrowsException()
        {
            Product.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            Assert.Throws<ArgumentException>(() =>
                new Product("Product", "Description", "SKU123", 0m, 50, 10, images));
        }

        [Fact]
        public void Product_NegativeStock_ThrowsException()
        {
            Product.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            Assert.Throws<ArgumentException>(() =>
                new Product("Product", "Description", "SKU123", 99.99m, -10, 10, images));
        }
    }
}

