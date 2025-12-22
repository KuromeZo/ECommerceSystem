using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ECommerceSystem.Models
{
    public class ShoppingCart
    {
        private static List<ShoppingCart> _extent = new List<ShoppingCart>();
        private static int _nextId = 1;

        public const int MAX_ITEMS = 100;

        [Key]
        public int CartId { get; private set; }

        public Customer Customer { get; set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModifiedDate { get; set; }

        private List<ItemInCart> _items;
        public IReadOnlyList<ItemInCart> Items => _items.AsReadOnly();

        public decimal PotentialSubtotal
        {
            get => _items.Sum(item => item.Subtotal);
        }

        public decimal PotentialTaxAmount
        {
            get => PotentialSubtotal * 0.1m;
        }

        public decimal PotentialShippingFee
        {
            get
            {
                if (PotentialSubtotal >= 100)
                    return 0;
                return 10.99m;
            }
        }

        public ShoppingCart(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            CartId = _nextId++;
            Customer = customer;
            _items = new List<ItemInCart>();
            CreatedDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;

            _extent.Add(this);
        }

        public void AddItem(ItemInCart item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (_items.Count >= MAX_ITEMS)
                throw new InvalidOperationException($"Cannot add more than {MAX_ITEMS} items");

            var existingItem = _items.FirstOrDefault(i => i.Product.ProductId == item.Product.ProductId);
            if (existingItem != null)
            {
                existingItem.UpdateQuantity(existingItem.Quantity + item.Quantity);
            }
            else
            {
                _items.Add(item);
            }

            LastModifiedDate = DateTime.Now;
        }

        public void RemoveItem(ItemInCart item)
        {
            _items.Remove(item);
            LastModifiedDate = DateTime.Now;
        }

        public void ClearCart()
        {
            _items.Clear();
            LastModifiedDate = DateTime.Now;
        }

        public static IReadOnlyList<ShoppingCart> GetExtent()
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

