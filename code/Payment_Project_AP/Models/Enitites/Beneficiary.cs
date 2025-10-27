using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.Models.Enitites
{
    public class Beneficiary //represents a recipient of funds(an individual or a company) managed by a corporate client for making payments.
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nickname { get; set; } // an easy-to-remember name for the client to identify the beneficiary

        [Required]
        [MaxLength(150)]
        public string AccountHolderName { get; set; }

        [Required]
        [MaxLength(20)]
        public string AccountNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string BankSwiftCode { get; set; } // the SWIFT/BIC code of the beneficiary's bank

        [MaxLength(100)]
        public string BankName { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //relationships
        // a beneficiary is owned by a specific corporate client
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
