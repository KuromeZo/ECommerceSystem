using System;
using System.Collections.Generic;
using Xunit;
using ECommerceSystem.Models;

namespace ECommerceSystem.Tests
{
    public class RestockOrderTests
    {
        [Fact]
        public void RestockOrder_ValidData_CreatesSuccessfully()
        {
            RestockOrder.ClearExtent();
            Product.ClearExtent();
            Supplier.ClearExtent();

            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 10, 5, images);
            var supplier = new Supplier("ABC Corp", "contact@abc.com", "555-1234");
            var restockOrder = new RestockOrder(product, supplier, 50);

            Assert.Equal(product, restockOrder.Product);
            Assert.Equal(supplier, restockOrder.Supplier);
            Assert.Equal(50, restockOrder.Quantity);
            Assert.Equal(RestockOrderStatus.Requested, restockOrder.Status);
        }

        [Fact]
        public void RestockOrder_NullProduct_ThrowsException()
        {
            RestockOrder.ClearExtent();
            Supplier.ClearExtent();
            var supplier = new Supplier("ABC Corp", "contact@abc.com", "555-1234");

            Assert.Throws<ArgumentNullException>(() =>
                new RestockOrder(null!, supplier, 50));
        }

        [Fact]
        public void RestockOrder_NullSupplier_ThrowsException()
        {
            RestockOrder.ClearExtent();
            Product.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 10, 5, images);

            Assert.Throws<ArgumentNullException>(() =>
                new RestockOrder(product, null!, 50));
        }

        [Fact]
        public void RestockOrder_ZeroQuantity_ThrowsException()
        {
            RestockOrder.ClearExtent();
            Product.ClearExtent();
            Supplier.ClearExtent();

            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 10, 5, images);
            var supplier = new Supplier("ABC Corp", "contact@abc.com", "555-1234");

            Assert.Throws<ArgumentException>(() =>
                new RestockOrder(product, supplier, 0));
        }

        [Fact]
        public void RestockOrder_UpdateStatus_UpdatesSuccessfully()
        {
            RestockOrder.ClearExtent();
            Product.ClearExtent();
            Supplier.ClearExtent();

            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 10, 5, images);
            var supplier = new Supplier("ABC Corp", "contact@abc.com", "555-1234");
            var restockOrder = new RestockOrder(product, supplier, 50);

            restockOrder.UpdateStatus(RestockOrderStatus.Confirmed);
            Assert.Equal(RestockOrderStatus.Confirmed, restockOrder.Status);
        }

        [Fact]
        public void RestockOrder_UpdateStatus_Received_UpdatesStock()
        {
            RestockOrder.ClearExtent();
            Product.ClearExtent();
            Supplier.ClearExtent();

            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 10, 5, images);
            var supplier = new Supplier("ABC Corp", "contact@abc.com", "555-1234");
            var restockOrder = new RestockOrder(product, supplier, 50);

            var initialStock = product.StockQuantity;
            restockOrder.UpdateStatus(RestockOrderStatus.Received);

            Assert.Equal(initialStock + 50, product.StockQuantity);
            Assert.NotNull(restockOrder.ActualDeliveryDate);
        }

        [Fact]
        public void RestockOrder_UpdateStatus_CompletedOrder_ThrowsException()
        {
            RestockOrder.ClearExtent();
            Product.ClearExtent();
            Supplier.ClearExtent();

            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 10, 5, images);
            var supplier = new Supplier("ABC Corp", "contact@abc.com", "555-1234");
            var restockOrder = new RestockOrder(product, supplier, 50);

            restockOrder.UpdateStatus(RestockOrderStatus.Received);

            Assert.Throws<InvalidOperationException>(() =>
                restockOrder.UpdateStatus(RestockOrderStatus.Confirmed));
        }

        [Fact]
        public void RestockOrder_CreateRestockOrder_StaticMethod_CreatesSuccessfully()
        {
            RestockOrder.ClearExtent();
            Product.ClearExtent();
            Supplier.ClearExtent();

            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 10, 5, images);
            var supplier = new Supplier("ABC Corp", "contact@abc.com", "555-1234");

            var restockOrder = RestockOrder.CreateRestockOrder(product, supplier, 50);

            Assert.NotNull(restockOrder);
            Assert.Equal(product, restockOrder.Product);
            Assert.Equal(supplier, restockOrder.Supplier);
            Assert.Equal(50, restockOrder.Quantity);
        }

        [Fact]
        public void RestockOrder_GetExtent_ReturnsAllRestockOrders()
        {
            RestockOrder.ClearExtent();
            Product.ClearExtent();
            Supplier.ClearExtent();

            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 10, 5, images);
            var supplier = new Supplier("ABC Corp", "contact@abc.com", "555-1234");
            var restockOrder1 = new RestockOrder(product, supplier, 50);
            var restockOrder2 = new RestockOrder(product, supplier, 30);

            var extent = RestockOrder.GetExtent();
            Assert.Equal(2, extent.Count);
        }
    }
}

