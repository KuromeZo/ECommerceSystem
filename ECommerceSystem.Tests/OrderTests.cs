using System;
using System.Collections.Generic;
using Xunit;
using ECommerceSystem.Models;

namespace ECommerceSystem.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Order_ValidData_CreatesSuccessfully()
        {
            Order.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var order = new Order(customer, 10m, 5m, "Handle with care");

            Assert.Equal(customer, order.Customer);
            Assert.Equal(10m, order.TaxAmount);
            Assert.Equal(5m, order.ShippingFee);
            Assert.Equal("Handle with care", order.SpecialInstructions);
            Assert.Equal(OrderStatus.Pending, order.Status);
        }

        [Fact]
        public void Order_CalculatesSubtotal_Correctly()
        {
            Order.ClearExtent();
            OrderedItem.ClearExtent();
            Product.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var order = new Order(customer, 10m, 5m);
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new OrderedItem(product, 2, 100m);

            order.AddOrderedItem(item);

            Assert.Equal(200m, order.Subtotal);
        }

        [Fact]
        public void Order_CalculatesTotal_Correctly()
        {
            Order.ClearExtent();
            OrderedItem.ClearExtent();
            Product.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var order = new Order(customer, 10m, 5m);
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new OrderedItem(product, 2, 100m);

            order.AddOrderedItem(item);

            Assert.Equal(215m, order.Total);
        }

        [Fact]
        public void Order_NullCustomer_ThrowsException()
        {
            Order.ClearExtent();
            Assert.Throws<ArgumentNullException>(() => new Order(null!, 10m, 5m));
        }

        [Fact]
        public void Order_NegativeTaxAmount_ThrowsException()
        {
            Order.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            Assert.Throws<ArgumentException>(() => new Order(customer, -10m, 5m));
        }

        [Fact]
        public void Order_NegativeShippingFee_ThrowsException()
        {
            Order.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            Assert.Throws<ArgumentException>(() => new Order(customer, 10m, -5m));
        }

        [Fact]
        public void Order_AddOrderedItem_AddsItemSuccessfully()
        {
            Order.ClearExtent();
            OrderedItem.ClearExtent();
            Product.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var order = new Order(customer, 10m, 5m);
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new OrderedItem(product, 2, 100m);

            order.AddOrderedItem(item);

            Assert.Single(order.OrderedItems);
        }

        [Fact]
        public void Order_AddOrderedItem_NullItem_ThrowsException()
        {
            Order.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var order = new Order(customer, 10m, 5m);

            Assert.Throws<ArgumentNullException>(() => order.AddOrderedItem(null!));
        }
    }
}

