namespace ECommerceSystem.Models
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }

    public enum ShipmentStatus
    {
        Preparing,
        InTransit,
        OutForDelivery,
        Delivered
    }

    public enum RestockOrderStatus
    {
        Requested,
        Confirmed,
        InTransit,
        Received,
        Cancelled
    }
}

