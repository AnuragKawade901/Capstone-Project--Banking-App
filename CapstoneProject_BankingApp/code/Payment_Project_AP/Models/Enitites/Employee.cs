using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment_Project_AP.Models.Enitites
{
    public class Employee //represents an employee of a corporate client, used for salary disbursement.
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string EmployeeCode { get; set; } // unique identifier within the client company

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string BankAccountNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string BankName { get; set; }

        [Required]
        [MaxLength(20)]
        public string BankSwiftCode { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //relationships
        //employee must belong to a corporate client
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
