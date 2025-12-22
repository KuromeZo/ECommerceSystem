using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceSystem.Models
{
    public abstract class Payment
    {
        private static List<Payment> _extent = new List<Payment>();
        private static int _nextId = 1;

        [Key]
        public int PaymentId { get; private set; }

        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }

        public Order Order { get; set; }

        protected Payment(decimal amount, Order order)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than 0");
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            PaymentId = _nextId++;
            Amount = amount;
            PaymentDate = DateTime.Now;
            Status = PaymentStatus.Pending;
            Order = order;

            _extent.Add(this);
        }

        public void ViewPaymentStatus()
        {
            Console.WriteLine($"Payment #{PaymentId} - Status: {Status}");
            Console.WriteLine($"Amount: ${Amount:F2}");
            Console.WriteLine($"Date: {PaymentDate:yyyy-MM-dd HH:mm}");
        }

        public static IReadOnlyList<Payment> GetExtent()
        {
            return _extent.AsReadOnly();
        }

        public static void ClearExtent()
        {
            _extent.Clear();
            _nextId = 1;
        }
    }
}

