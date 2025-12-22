using System;
using Xunit;
using ECommerceSystem.Models;

namespace ECommerceSystem.Tests
{
    public class AddressTests
    {
        [Fact]
        public void Address_ValidData_CreatesSuccessfully()
        {
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            Assert.Equal("123 Main St", address.Street);
            Assert.Equal("New York", address.City);
            Assert.Equal("NY", address.State);
            Assert.Equal("10001", address.PostalCode);
            Assert.Equal("USA", address.Country);
        }

        [Fact]
        public void Address_EmptyStreet_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Address("", "New York", "NY", "10001", "USA"));
        }

        [Fact]
        public void Address_EmptyCity_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Address("123 Main St", "", "NY", "10001", "USA"));
        }

        [Fact]
        public void Address_EmptyState_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Address("123 Main St", "New York", "", "10001", "USA"));
        }

        [Fact]
        public void Address_EmptyPostalCode_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Address("123 Main St", "New York", "NY", "", "USA"));
        }

        [Fact]
        public void Address_EmptyCountry_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Address("123 Main St", "New York", "NY", "10001", ""));
        }

        [Fact]
        public void Address_ToString_ReturnsFormattedString()
        {
            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var result = address.ToString();
            Assert.Contains("123 Main St", result);
            Assert.Contains("New York", result);
            Assert.Contains("NY", result);
            Assert.Contains("10001", result);
            Assert.Contains("USA", result);
        }
    }
}

