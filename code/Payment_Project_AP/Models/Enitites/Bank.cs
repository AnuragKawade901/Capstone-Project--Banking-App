using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.Models.Enitites
{
        public class Bank
        {
            public int Id { get; set; }

            [Required]
            [MaxLength(100)]
            public string Name { get; set; }

            [Required]
            [MaxLength(20)]
            public string SwiftCode { get; set; } // also known as BIC

            [MaxLength(255)]
            public string Address { get; set; }

            public bool IsActive { get; set; } = true;

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public DateTime? UpdatedAt { get; set; }

            //relationships
            // a Bank can have many Bank Users
            public ICollection<User> Users { get; set; }

            // a Bank can have many corporate clients
            public ICollection<Client> Clients { get; set; }

            public Bank()
            {
                Users = new HashSet<User>();
                Clients = new HashSet<Client>();
            }
        }
    }

