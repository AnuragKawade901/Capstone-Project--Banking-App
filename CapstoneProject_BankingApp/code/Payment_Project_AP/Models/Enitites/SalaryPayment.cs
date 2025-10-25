using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace Payment_Project_AP.Models.Enitites
{
    public class SalaryPayment //represents the individual payment to a single employee as part of a larger SalaryDisbursement batch.
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        // ee-using the PaymentStatus enum as it fits the purpose (eg processed, failed)
        [Required]
        public PaymentStatus Status { get; set; } = PaymentStatus.PendingApproval;

        [MaxLength(255)]
        public string FailureReason { get; set; } // to store error messages if a payment fails

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAt { get; set; }

        //relationships
        // the batch this payment belongs to
        [Required]
        public int SalaryDisbursementId { get; set; }
        public SalaryDisbursement SalaryDisbursement { get; set; }

        // the employee who receives this payment
        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        // the resulting financial transaction record upon success
        public int? TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
