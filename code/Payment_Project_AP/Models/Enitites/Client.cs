using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Payment_Project_AP.Models.Enitites
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required]
        [MaxLength(150)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(50)]
        public string RegistrationNumber { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string ContactEmail { get; set; }

        [MaxLength(20)]
        public string ContactPhone { get; set; }

        public VerificationStatus VerificationStatus { get; set; } = VerificationStatus.Pending;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        //relationships
        // a client must belong to a bank
        [Required]
        public int BankId { get; set; }
        public Bank Bank { get; set; }

        // a client has its own set of users (ClientUsers)
        public ICollection<User> Users { get; set; }

        // a client can have multiple bank accounts
        public ICollection<Account> Accounts { get; set; }

        // a client manages a list of beneficiaries
        public ICollection<Beneficiary> Beneficiaries { get; set; }

        // a Client manages its employees for salary disbursement
        public ICollection<Employee> Employees { get; set; }

        public Client()
        {
            Users = new HashSet<User>();
            Accounts = new HashSet<Account>();
            Beneficiaries = new HashSet<Beneficiary>();
            Employees = new HashSet<Employee>();
        }
    }
}
