using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceSystem.Models
{
    public class Address
    {
        [Required(ErrorMessage = "Street is required")]
        [StringLength(200, MinimumLength = 1)]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(100, MinimumLength = 1)]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(100, MinimumLength = 1)]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(100, MinimumLength = 1)]
        public string Country { get; set; }

        public Address(string street, string city, string state, string postalCode, string country)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be empty");
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City cannot be empty");
            if (string.IsNullOrWhiteSpace(state))
                throw new ArgumentException("State cannot be empty");
            if (string.IsNullOrWhiteSpace(postalCode))
                throw new ArgumentException("Postal code cannot be empty");
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country cannot be empty");

            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {State} {PostalCode}, {Country}";
        }
    }
}

