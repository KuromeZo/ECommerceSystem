using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceSystem.Models
{
    public abstract class User
    {
        private static List<User> _extent = new List<User>();
        private static int _nextId = 1;

        [Key]
        public int UserId { get; private set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string PasswordHash { get; set; }

        public DateTime CreatedDate { get; set; }

        protected User(string username, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty");
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password cannot be empty");

            UserId = _nextId++;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            CreatedDate = DateTime.Now;

            _extent.Add(this);
        }

        public static IReadOnlyList<User> GetExtent()
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

