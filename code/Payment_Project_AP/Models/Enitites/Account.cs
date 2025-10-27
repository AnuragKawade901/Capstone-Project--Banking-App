using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace Payment_Project_AP.Models.Enitites
{
  public class Account
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string AccountNumber { get; set; }

        [Required]
        public AccountType AccountType { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")] // specifies precision for financial data in the database
        public decimal Balance { get; set; }

        [Required]
        [MaxLength(3)]
        public string Currency { get; set; } //inr, usd

        public Enums.AccountStatus Status { get; set; } = Enums.AccountStatus.Active;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
        //realtionships
        // an account must belong to one corporate client
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        // an account can be the source for many transactions
        public ICollection<Transaction> SourceTransactions { get; set; }

        // an account can be the destination for many transactions
        public ICollection<Transaction> DestinationTransactions { get; set; }

        public Account()
        {
            SourceTransactions = new HashSet<Transaction>();
            DestinationTransactions = new HashSet<Transaction>();
        }
    }
}
