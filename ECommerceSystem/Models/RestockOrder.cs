using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceSystem.Models
{
    public class RestockOrder
    {
        private static List<RestockOrder> _extent = new List<RestockOrder>();
        private static int _nextId = 1;

        [Key]
        public int RestockOrderId { get; private set; }

        public Product Product { get; set; }
        public Supplier Supplier { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public RestockOrderStatus Status { get; set; }
        public DateTime RequestedDate { get; private set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }

        public RestockOrder(Product product, Supplier supplier, int quantity, DateTime? expectedDeliveryDate = null)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));
            if (supplier == null)
                throw new ArgumentNullException(nameof(supplier));
            if (quantity < 1)
                throw new ArgumentException("Quantity must be at least 1");
            if (expectedDeliveryDate.HasValue && expectedDeliveryDate.Value < DateTime.Now)
                throw new ArgumentException("Expected delivery date cannot be in the past");

            RestockOrderId = _nextId++;
            Product = product;
            Supplier = supplier;
            Quantity = quantity;
            Status = RestockOrderStatus.Requested;
            RequestedDate = DateTime.Now;
            ExpectedDeliveryDate = expectedDeliveryDate;

            _extent.Add(this);
        }

        public static RestockOrder CreateRestockOrder(Product product, Supplier supplier, int quantity)
        {
            return new RestockOrder(product, supplier, quantity);
        }

        public void UpdateStatus(RestockOrderStatus newStatus)
        {
            if (Status == RestockOrderStatus.Received || Status == RestockOrderStatus.Cancelled)
                throw new InvalidOperationException("Cannot update completed or cancelled orders");

            if (newStatus == RestockOrderStatus.Received)
            {
                ActualDeliveryDate = DateTime.Now;
                Product.StockQuantity += Quantity;
            }

            Status = newStatus;
        }

        public static IReadOnlyList<RestockOrder> GetExtent()
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

