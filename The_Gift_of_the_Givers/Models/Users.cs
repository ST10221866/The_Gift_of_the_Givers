using System;
using System.ComponentModel.DataAnnotations;

namespace The_Gift_of_the_Givers.Models
{
    public class Users
    {
        [Key]
        public Guid UserID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Username { get; set; } // For login

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; } // Store hashed password

        [Required]
        [EmailAddress]
        public string Email { get; set; } // Contact info


        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [Phone]
        public string ContactNumber { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } // When the user was created

        [Required]
        public bool IsActive { get; set; } // Account status

        // Uncomment if you want to manage roles for the user
        // public UserRole Role { get; set; } 
    }

    public enum UserRole
    {
        Admin,
        Volunteer,
        Donor
    }
}
