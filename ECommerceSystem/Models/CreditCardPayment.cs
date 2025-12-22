using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceSystem.Models
{
    public class CreditCardPayment : Payment
    {
        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string CardholderName { get; set; }

        public DateTime ExpiryDate { get; set; }

        public CreditCardPayment(decimal amount, Order order, string cardNumber, 
                                string cardholderName, DateTime expiryDate)
            : base(amount, order)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                throw new ArgumentException("Card number cannot be empty");
            if (string.IsNullOrWhiteSpace(cardholderName))
                throw new ArgumentException("Cardholder name cannot be empty");
            if (expiryDate < DateTime.Now)
                throw new ArgumentException("Card has expired");

            CardNumber = cardNumber;
            CardholderName = cardholderName;
            ExpiryDate = expiryDate;
        }
    }
}

