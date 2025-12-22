using System;
using System.Collections.Generic;
using Xunit;
using ECommerceSystem.Models;

namespace ECommerceSystem.Tests
{
    public class ShoppingCartTests
    {
        [Fact]
        public void ShoppingCart_ClassAttribute_MaxItemsIs100()
        {
            Assert.Equal(100, ShoppingCart.MAX_ITEMS);
        }

        [Fact]
        public void ShoppingCart_CalculatesPotentialSubtotal_Correctly()
        {
            ShoppingCart.ClearExtent();
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var cart = new ShoppingCart(customer);
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new ItemInCart(product, 2);

            cart.AddItem(item);

            Assert.Equal(200m, cart.PotentialSubtotal);
        }

        [Fact]
        public void ShoppingCart_CalculatesPotentialTaxAmount_Correctly()
        {
            ShoppingCart.ClearExtent();
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var cart = new ShoppingCart(customer);
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new ItemInCart(product, 1);

            cart.AddItem(item);

            Assert.Equal(10m, cart.PotentialTaxAmount);
        }

        [Fact]
        public void ShoppingCart_CalculatesShippingFee_FreeShippingOver100()
        {
            ShoppingCart.ClearExtent();
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var cart = new ShoppingCart(customer);
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new ItemInCart(product, 1);

            cart.AddItem(item);

            Assert.Equal(0m, cart.PotentialShippingFee);
        }

        [Fact]
        public void ShoppingCart_CalculatesShippingFee_ChargesUnder100()
        {
            ShoppingCart.ClearExtent();
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var cart = new ShoppingCart(customer);
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 50m, 50, 10, images);
            var item = new ItemInCart(product, 1);

            cart.AddItem(item);

            Assert.Equal(10.99m, cart.PotentialShippingFee);
        }

        [Fact]
        public void ShoppingCart_NullCustomer_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new ShoppingCart(null!));
        }

        [Fact]
        public void ShoppingCart_AddItem_AddsItemSuccessfully()
        {
            ShoppingCart.ClearExtent();
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var cart = new ShoppingCart(customer);
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new ItemInCart(product, 1);

            cart.AddItem(item);

            Assert.Single(cart.Items);
        }

        [Fact]
        public void ShoppingCart_AddDuplicateItem_UpdatesQuantity()
        {
            ShoppingCart.ClearExtent();
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var cart = new ShoppingCart(customer);
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item1 = new ItemInCart(product, 1);
            var item2 = new ItemInCart(product, 2);

            cart.AddItem(item1);
            cart.AddItem(item2);

            Assert.Single(cart.Items);
            Assert.Equal(3, cart.Items[0].Quantity);
        }

        [Fact]
        public void ShoppingCart_ClearCart_RemovesAllItems()
        {
            ShoppingCart.ClearExtent();
            Product.ClearExtent();
            ItemInCart.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var cart = new ShoppingCart(customer);
            var images = new List<string> { "image1.jpg" };
            var product = new Product("Laptop", "Description", "SKU123", 100m, 50, 10, images);
            var item = new ItemInCart(product, 1);

            cart.AddItem(item);
            cart.ClearCart();

            Assert.Empty(cart.Items);
        }
    }
}

