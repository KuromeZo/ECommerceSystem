using System;
using System.Collections.Generic;
using Xunit;
using ECommerceSystem.Models;

namespace ECommerceSystem.Tests
{
    public class ItemInCartTests
    {
        [Fact]
        public void ItemInCart_ValidData_CreatesSuccessfully()
        {
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new ItemInCart(product, 2);

            Assert.Equal(product, item.Product);
            Assert.Equal(2, item.Quantity);
            Assert.Equal(0m, item.DiscountApplied);
        }

        [Fact]
        public void ItemInCart_CalculatesSubtotal_Correctly()
        {
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new ItemInCart(product, 2);

            Assert.Equal(200m, item.Subtotal);
        }

        [Fact]
        public void ItemInCart_CalculatesSubtotal_WithDiscount()
        {
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new ItemInCart(product, 2);

            item.ApplyPotentialDiscount(10m);
            Assert.Equal(180m, item.Subtotal);
        }

        [Fact]
        public void ItemInCart_NullProduct_ThrowsException()
        {
            ItemInCart.ClearExtent();
            Assert.Throws<ArgumentNullException>(() => new ItemInCart(null!, 1));
        }

        [Fact]
        public void ItemInCart_ZeroQuantity_ThrowsException()
        {
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            Assert.Throws<ArgumentException>(() => new ItemInCart(product, 0));
        }

        [Fact]
        public void ItemInCart_QuantityExceedsStock_ThrowsException()
        {
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 10, 5, images);
            Assert.Throws<InvalidOperationException>(() => new ItemInCart(product, 20));
        }

        [Fact]
        public void ItemInCart_UpdateQuantity_UpdatesSuccessfully()
        {
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new ItemInCart(product, 2);

            item.UpdateQuantity(5);
            Assert.Equal(5, item.Quantity);
        }

        [Fact]
        public void ItemInCart_ApplyDiscount_NegativeDiscount_ThrowsException()
        {
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new ItemInCart(product, 2);

            Assert.Throws<ArgumentException>(() => item.ApplyPotentialDiscount(-10m));
        }

        [Fact]
        public void ItemInCart_ApplyDiscount_ExceedsPrice_ThrowsException()
        {
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new ItemInCart(product, 2);

            Assert.Throws<ArgumentException>(() => item.ApplyPotentialDiscount(150m));
        }
    }
}

