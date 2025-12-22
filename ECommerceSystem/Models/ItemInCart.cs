using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceSystem.Models
{
    public class ItemInCart
    {
        private static List<ItemInCart> _extent = new List<ItemInCart>();

        public Product Product { get; set; }

        private int _quantity;
        [Range(1, int.MaxValue)]
        public int Quantity
        {
            get => _quantity;
            private set
            {
                if (value < 1)
                    throw new ArgumentException("Quantity must be at least 1");
                _quantity = value;
            }
        }

        [Range(0, double.MaxValue)]
        public decimal DiscountApplied { get; set; }

        public decimal Subtotal
        {
            get => (Product.Price - DiscountApplied) * Quantity;
        }

        public ItemInCart(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (quantity < 1)
                throw new ArgumentException("Quantity must be at least 1");
            if (quantity > product.StockQuantity)
                throw new InvalidOperationException($"Only {product.StockQuantity} items available");

            Product = product;
            Quantity = quantity;
            DiscountApplied = 0;

            _extent.Add(this);
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 1)
                throw new ArgumentException("Quantity must be at least 1");
            if (newQuantity > Product.StockQuantity)
                throw new InvalidOperationException($"Only {Product.StockQuantity} items available");

            _quantity = newQuantity;
        }

        public void ApplyPotentialDiscount(decimal discount)
        {
            if (discount < 0)
                throw new ArgumentException("Discount cannot be negative");
            if (discount > Product.Price)
                throw new ArgumentException("Discount cannot exceed product price");

            DiscountApplied = discount;
        }

        public static IReadOnlyList<ItemInCart> GetExtent()
        {
            return _extent.AsReadOnly();
        }

        public static void ClearExtent()
        {
            _extent.Clear();
        }
    }
}

