using System;
using Xunit;
using ECommerceSystem.Models;

namespace ECommerceSystem.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_ValidData_CreatesSuccessfully()
        {
            User.ClearExtent();
            Customer.ClearCustomerExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);

            Assert.Equal("johndoe", customer.Username);
            Assert.Equal("john@example.com", customer.Email);
            Assert.Equal("John", customer.FirstName);
            Assert.Equal("Doe", customer.LastName);
            Assert.Equal("555-1234", customer.PhoneNumber);
            Assert.Equal(0, customer.LoyaltyPoints);
            Assert.NotNull(customer.ShippingAddress);
        }

        [Fact]
        public void Customer_EmptyFirstName_ThrowsException()
        {
            User.ClearExtent();
            Customer.ClearCustomerExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            Assert.Throws<ArgumentException>(() =>
                new Customer("johndoe", "john@example.com", "password123",
                           "", "Doe", "555-1234", address));
        }

        [Fact]
        public void Customer_EmptyLastName_ThrowsException()
        {
            User.ClearExtent();
            Customer.ClearCustomerExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            Assert.Throws<ArgumentException>(() =>
                new Customer("johndoe", "john@example.com", "password123",
                           "John", "", "555-1234", address));
        }

        [Fact]
        public void Customer_NullShippingAddress_ThrowsException()
        {
            User.ClearExtent();
            Customer.ClearCustomerExtent();
            Assert.Throws<ArgumentNullException>(() =>
                new Customer("johndoe", "john@example.com", "password123",
                           "John", "Doe", "555-1234", null!));
        }

        [Fact]
        public void Customer_EarnLoyaltyPoints_IncreasesPoints()
        {
            User.ClearExtent();
            Customer.ClearCustomerExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);

            customer.EarnLoyaltyPoints(100);
            Assert.Equal(100, customer.LoyaltyPoints);

            customer.EarnLoyaltyPoints(50);
            Assert.Equal(150, customer.LoyaltyPoints);
        }

        [Fact]
        public void Customer_EarnLoyaltyPoints_NegativePoints_ThrowsException()
        {
            User.ClearExtent();
            Customer.ClearCustomerExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);

            Assert.Throws<ArgumentException>(() => customer.EarnLoyaltyPoints(-10));
        }

        [Fact]
        public void Customer_ApplyLoyaltyPoints_DecreasesPoints()
        {
            User.ClearExtent();
            Customer.ClearCustomerExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);

            customer.EarnLoyaltyPoints(100);
            customer.ApplyLoyaltyPoints(30);
            Assert.Equal(70, customer.LoyaltyPoints);
        }

        [Fact]
        public void Customer_ApplyLoyaltyPoints_InsufficientPoints_ThrowsException()
        {
            User.ClearExtent();
            Customer.ClearCustomerExtent();
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);

            customer.EarnLoyaltyPoints(50);
            Assert.Throws<InvalidOperationException>(() => customer.ApplyLoyaltyPoints(100));
        }

        [Fact]
        public void Customer_GetCustomerExtent_ReturnsAllCustomers()
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
        }
    }
}

