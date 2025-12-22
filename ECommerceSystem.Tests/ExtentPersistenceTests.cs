using System;
using System.Collections.Generic;
using Xunit;
using ECommerceSystem.Models;

namespace ECommerceSystem.Tests
{
    public class ExtentPersistenceTests
    {
        [Fact]
        public void ProductExtent_StoreMultipleProducts_PersistsCorrectly()
        {
            Product.ClearExtent();
            var images = new List<string> { "image1.jpg" };

            var product1 = new Product("Laptop", "Gaming", "SKU001", 1299m, 10, 5, images);
            var product2 = new Product("Mouse", "Wireless", "SKU002", 29m, 50, 10, images);

            var extent = Product.GetExtent();

            Assert.Equal(2, extent.Count);
            Assert.Contains(product1, extent);
            Assert.Contains(product2, extent);
        }

        [Fact]
        public void ExtentPersistence_ClearExtent_RemovesAllObjects()
        {
            Product.ClearExtent();
            var images = new List<string> { "image1.jpg" };
            var product1 = new Product("Laptop", "Gaming", "SKU001", 1299m, 10, 5, images);

            Product.ClearExtent();

            Assert.Empty(Product.GetExtent());
        }

        [Fact]
        public void CustomerExtent_StoreMultipleCustomers_PersistsCorrectly()
        {
            User.ClearExtent();
            Customer.ClearCustomerExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");

            var customer1 = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var customer2 = new Customer("janedoe", "jane@example.com", "password456",
                                       "Jane", "Doe", "555-5678", address);

            var extent = Customer.GetCustomerExtent();

            Assert.Equal(2, extent.Count);
            Assert.Contains(customer1, extent);
            Assert.Contains(customer2, extent);
        }

        [Fact]
        public void SupplierExtent_StoreMultipleSuppliers_PersistsCorrectly()
        {
            Supplier.ClearExtent();

            var supplier1 = new Supplier("ABC Corp", "contact@abc.com", "555-1234");
            var supplier2 = new Supplier("XYZ Inc", "contact@xyz.com", "555-5678");

            var extent = Supplier.GetExtent();

            Assert.Equal(2, extent.Count);
            Assert.Contains(supplier1, extent);
            Assert.Contains(supplier2, extent);
        }

        [Fact]
        public void OrderExtent_StoreMultipleOrders_PersistsCorrectly()
        {
            Order.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);

            var order1 = new Order(customer, 10m, 5m);
            var order2 = new Order(customer, 15m, 8m);

            var extent = Order.GetExtent();

            Assert.Equal(2, extent.Count);
            Assert.Contains(order1, extent);
            Assert.Contains(order2, extent);
        }

        [Fact]
        public void ShoppingCartExtent_StoreMultipleCarts_PersistsCorrectly()
        {
            ShoppingCart.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer1 = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var customer2 = new Customer("janedoe", "jane@example.com", "password456",
                                       "Jane", "Doe", "555-5678", address);

            var cart1 = new ShoppingCart(customer1);
            var cart2 = new ShoppingCart(customer2);

            var extent = ShoppingCart.GetExtent();

            Assert.Equal(2, extent.Count);
            Assert.Contains(cart1, extent);
            Assert.Contains(cart2, extent);
        }

        [Fact]
        public void PaymentExtent_StoreMultiplePayments_PersistsCorrectly()
        {
            Payment.ClearExtent();
            Order.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var order1 = new Order(customer, 10m, 5m);
            var order2 = new Order(customer, 10m, 5m);
            var expiryDate = DateTime.Now.AddYears(1);

            var payment1 = new CreditCardPayment(100m, order1, "4111111111111111", "John Doe", expiryDate);
            var payment2 = new CreditCardPayment(200m, order2, "4111111111111111", "John Doe", expiryDate);

            var extent = Payment.GetExtent();

            Assert.Equal(2, extent.Count);
            Assert.Contains(payment1, extent);
            Assert.Contains(payment2, extent);
        }

        [Fact]
        public void RestockOrderExtent_StoreMultipleRestockOrders_PersistsCorrectly()
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
            Assert.Contains(restockOrder1, extent);
            Assert.Contains(restockOrder2, extent);
        }

        [Fact]
        public void UserExtent_IncludesAllUserSubclasses()
        {
            User.ClearExtent();
            Customer.ClearCustomerExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");

            var customer1 = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var customer2 = new Customer("janedoe", "jane@example.com", "password456",
                                       "Jane", "Doe", "555-5678", address);

            var extent = User.GetExtent();

            Assert.Equal(2, extent.Count);
            Assert.Contains(customer1, extent);
            Assert.Contains(customer2, extent);
        }
    }
}

