using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment_Project_AP.Models.Enitites
{
    public class SalaryDisbursement //represents a batch salary disbursement process for multiple employees.
    {
        public int Id { get; set; }

        public DateTime DisbursementDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }

        public int TotalEmployees { get; set; }

        [Required]
        public Enums.DisbursementStatus Status { get; set; } = DisbursementStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; } // timestamp for when the entire batch is completed or failed

        //relationships
        // the corporate client initiating the disbursement
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        // the account from which salaries will be debited
        [Required]
        public int SourceAccountId { get; set; }
        public Account SourceAccount { get; set; }

        // the ClientUser who initiated this batch process
        [Required]
        public int InitiatedByUserId { get; set; }
        public User InitiatedByUser { get; set; }

        // the individual salary payments included in this batch
        public ICollection<SalaryPayment> SalaryPayments { get; set; }

        public SalaryDisbursement()
        {
            SalaryPayments = new HashSet<SalaryPayment>();
        }
    }
}
