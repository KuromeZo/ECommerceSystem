using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceSystem.Models
{
    public class OrderedItem
    {
        private static List<OrderedItem> _extent = new List<OrderedItem>();

        public Product Product { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal DiscountApplied { get; set; }

        public decimal Subtotal
        {
            get => (UnitPrice - DiscountApplied) * Quantity;
        }

        public OrderedItem(Product product, int quantity, decimal unitPrice)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity < 1)
                throw new ArgumentException("Quantity must be at least 1");
            if (unitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative");

            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DiscountApplied = 0;

            _extent.Add(this);
        }

        public void ApplyDiscount(decimal discount)
        {
            if (discount < 0)
                throw new ArgumentException("Discount cannot be negative");
            if (discount > UnitPrice)
                throw new ArgumentException("Discount cannot exceed unit price");

            DiscountApplied = discount;
        }

        public static IReadOnlyList<OrderedItem> GetExtent()
        {
            return _extent.AsReadOnly();
        }

        public static void ClearExtent()
        {
            _extent.Clear();
        }
    }
}

