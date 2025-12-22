using System;
using System.Collections.Generic;
using Xunit;
using ECommerceSystem.Models;

namespace ECommerceSystem.Tests
{
    public class PaymentTests
    {
        [Fact]
        public void CreditCardPayment_ValidData_CreatesSuccessfully()
        {
            Payment.ClearExtent();
            Order.ClearExtent();
            OrderedItem.ClearExtent();
            Product.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var order = new Order(customer, 10m, 5m);
            var expiryDate = DateTime.Now.AddYears(1);
            var payment = new CreditCardPayment(100m, order, "4111111111111111",
                                               "John Doe", expiryDate);

            Assert.Equal(100m, payment.Amount);
            Assert.Equal(order, payment.Order);
            Assert.Equal("4111111111111111", payment.CardNumber);
            Assert.Equal("John Doe", payment.CardholderName);
            Assert.Equal(PaymentStatus.Pending, payment.Status);
        }

        [Fact]
        public void CreditCardPayment_ZeroAmount_ThrowsException()
        {
            Payment.ClearExtent();
            Order.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var order = new Order(customer, 10m, 5m);
            var expiryDate = DateTime.Now.AddYears(1);

            Assert.Throws<ArgumentException>(() =>
                new CreditCardPayment(0m, order, "4111111111111111", "John Doe", expiryDate));
        }

        [Fact]
        public void CreditCardPayment_NullOrder_ThrowsException()
        {
            Payment.ClearExtent();
            var expiryDate = DateTime.Now.AddYears(1);

            Assert.Throws<ArgumentNullException>(() =>
                new CreditCardPayment(100m, null!, "4111111111111111", "John Doe", expiryDate));
        }

        [Fact]
        public void CreditCardPayment_EmptyCardNumber_ThrowsException()
        {
            Payment.ClearExtent();
            Order.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var order = new Order(customer, 10m, 5m);
            var expiryDate = DateTime.Now.AddYears(1);

            Assert.Throws<ArgumentException>(() =>
                new CreditCardPayment(100m, order, "", "John Doe", expiryDate));
        }

        [Fact]
        public void CreditCardPayment_ExpiredCard_ThrowsException()
        {
            Payment.ClearExtent();
            Order.ClearExtent();
            Customer.ClearCustomerExtent();
            User.ClearExtent();

            var address = new Address("123 Main St", "New York", "NY", "10001", "USA");
            var customer = new Customer("johndoe", "john@example.com", "password123",
                                       "John", "Doe", "555-1234", address);
            var order = new Order(customer, 10m, 5m);
            var expiryDate = DateTime.Now.AddDays(-1);

            Assert.Throws<ArgumentException>(() =>
                new CreditCardPayment(100m, order, "4111111111111111", "John Doe", expiryDate));
        }

        [Fact]
        public void Payment_GetExtent_ReturnsAllPayments()
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
        }
    }
}

