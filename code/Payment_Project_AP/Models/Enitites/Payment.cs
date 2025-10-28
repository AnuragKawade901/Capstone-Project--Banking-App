using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace Payment_Project_AP.Models.Enitites
{
    public class Payment //represents a single payment instruction that requires approval before processing.
    {
        public int PaymentId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(3)]
        public string Currency { get; set; } // e.g., "USD", "EUR"

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public PaymentStatus Status { get; set; } = PaymentStatus.PendingApproval;

        [Required]
        public PaymentType PaymentType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ProcessedAt { get; set; } // timestamp for when the payment was approved/rejected

        //relationships
        // the corporate client initiating the payment
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        // the account from which the funds will be debited
        [Required]
        public int SourceAccountId { get; set; }
        public Account SourceAccount { get; set; }

        // the recipient of the funds
        [Required]
        public int BeneficiaryId { get; set; }
        public Beneficiary Beneficiary { get; set; }

        // the ClientUser who created this payment request
        [Required]
        public int InitiatedByUserId { get; set; }
        public User InitiatedByUser { get; set; }

        // the BankUser who approved or rejected the payment
        public int? ApprovedByUserId { get; set; }
        public User ApprovedByUser { get; set; }

        // once processed this links to the actual financial transaction record
        public int? TransactionId { get; set; }
        public Transaction Transaction { get; set; }

    }
}
