using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.Models.Enitites
{
        public class User
        {
            public int Id { get; set; }

            [Required]
            [MaxLength(50)]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [MaxLength(100)]
            public string Email { get; set; }

            // store the hashed password, never the plain text
            [Required]
            public byte[] PasswordHash { get; set; }

            // the salt used for hashing the password
            [Required]
            public byte[] PasswordSalt { get; set; }

            [MaxLength(50)]
            public string FirstName { get; set; }

            [MaxLength(50)]
            public string LastName { get; set; }

            [Required]
            public UserRole Role { get; set; }

            public bool IsActive { get; set; } = true;

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public DateTime? LastLogin { get; set; }

        //relationships
        // a Bank User belongs to one Bank
            public int? BankId { get; set; }
            public Bank Bank { get; set; }

            // a Client User belongs to one Client
            public int? ClientId { get; set; }
            public Client Client { get; set; }
        }
    }

