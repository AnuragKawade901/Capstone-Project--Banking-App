using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Payment_Project_AP.Models.Enitites
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(50)]
        public string RegistrationNumber { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string ContactEmail { get; set; }

        [MaxLength(20)]
        public string ContactPhone { get; set; }

        [MaxLength(255)]
        public string Address { get; set; }

        // tracks the status of the onboarding application
        public VerificationStatus Status { get; set; } = VerificationStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        //relationships
        // the Bank that is onboarding this customer
        [Required]
        public int BankId { get; set; }
        public Bank Bank { get; set; }

        // the BankUser who submitted this onboarding request
        public int SubmittedByUserId { get; set; }
        public User SubmittedByUser { get; set; }

        // documents submitted for verification like adhar card, pan card
        public ICollection<Document> Documents { get; set; }

        public Customer()
        {
            Documents = new HashSet<Document>();
        }
    }
}
