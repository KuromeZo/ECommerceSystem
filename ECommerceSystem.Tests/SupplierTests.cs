using System;
using Xunit;
using ECommerceSystem.Models;

namespace ECommerceSystem.Tests
{
    public class SupplierTests
    {
        [Fact]
        public void Supplier_ValidData_CreatesSuccessfully()
        {
            Supplier.ClearExtent();
            var supplier = new Supplier("ABC Corp", "contact@abc.com", "555-1234");
            Assert.Equal("ABC Corp", supplier.Name);
            Assert.Equal("contact@abc.com", supplier.ContactEmail);
            Assert.Equal("555-1234", supplier.ContactPhone);
            Assert.Null(supplier.Rating);
        }

        [Fact]
        public void Supplier_LeaveRating_SetsRating()
        {
            Supplier.ClearExtent();
            var supplier = new Supplier("ABC Corp", "contact@abc.com", "555-1234");
            supplier.LeaveRating(4.5m);
            Assert.Equal(4.5m, supplier.Rating);
        }

        [Fact]
        public void Supplier_LeaveRating_InvalidRating_ThrowsException()
        {
            Supplier.ClearExtent();
            var supplier = new Supplier("ABC Corp", "contact@abc.com", "555-1234");
            Assert.Throws<ArgumentOutOfRangeException>(() => supplier.LeaveRating(6m));
            Assert.Throws<ArgumentOutOfRangeException>(() => supplier.LeaveRating(-1m));
        }

        [Fact]
        public void Supplier_RatingProperty_ValidRange_AcceptsValue()
        {
            Supplier.ClearExtent();
            var supplier = new Supplier("ABC Corp", "contact@abc.com", "555-1234");
            supplier.Rating = 3.5m;
            Assert.Equal(3.5m, supplier.Rating);
        }

        [Fact]
        public void Supplier_RatingProperty_InvalidRange_ThrowsException()
        {
            Supplier.ClearExtent();
            var supplier = new Supplier("ABC Corp", "contact@abc.com", "555-1234");
            Assert.Throws<ArgumentOutOfRangeException>(() => supplier.Rating = 6m);
            Assert.Throws<ArgumentOutOfRangeException>(() => supplier.Rating = -1m);
        }

        [Fact]
        public void Supplier_EmptyName_ThrowsException()
        {
            Supplier.ClearExtent();
            Assert.Throws<ArgumentException>(() =>
                new Supplier("", "contact@abc.com", "555-1234"));
        }

        [Fact]
        public void Supplier_EmptyEmail_ThrowsException()
        {
            Supplier.ClearExtent();
            Assert.Throws<ArgumentException>(() =>
                new Supplier("ABC Corp", "", "555-1234"));
        }

        [Fact]
        public void Supplier_GetExtent_ReturnsAllSuppliers()
        {
            Supplier.ClearExtent();
            var supplier1 = new Supplier("ABC Corp", "contact@abc.com", "555-1234");
            var supplier2 = new Supplier("XYZ Inc", "contact@xyz.com", "555-5678");

            var extent = Supplier.GetExtent();
            Assert.Equal(2, extent.Count);
        }
    }
}

