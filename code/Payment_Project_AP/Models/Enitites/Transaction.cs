using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment_Project_AP.Models.Enitites
{
    public class Transaction
    /// represents an immutable financial transaction record in the ledger.
    /// this is created upon the successful processing of a payment.
    {
        public int Id { get; set; }

        // a unique reference number for tracking and auditing
        [Required]
        public Guid ReferenceNumber { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(3)]
        public string Currency { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        //relationships
        // the account from which funds were debited. Can be null for deposits from external sources.
        public int? SourceAccountId { get; set; }
        [InverseProperty("SourceTransactions")]
        public Account SourceAccount { get; set; }

        // the account to which funds were credited. Can be null for withdrawals to external sources.
        public int? DestinationAccountId { get; set; }
        [InverseProperty("DestinationTransactions")]
        public Account DestinationAccount { get; set; }
    }
}
