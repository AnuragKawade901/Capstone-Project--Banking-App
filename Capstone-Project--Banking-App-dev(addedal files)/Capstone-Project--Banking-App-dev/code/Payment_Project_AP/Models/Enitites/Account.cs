using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace Payment_Project_AP.Models.Enitites
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }                  // unique id for each account

        [Required(ErrorMessage = "Account Number is Required!")]
        [RegularExpression(@"^BAC\d{8}[A-Z0-9]{6}$", ErrorMessage = "Account Number is Not Valid")]
        public string AccountNumber { get; set; }           // unique account number following BAC format

        [ForeignKey("Client")]
        public int? ClientId { get; set; }                  // linked client id (nullable if not assigned yet)
        public virtual Client? Client { get; set; }         // navigation property to client details

        [ForeignKey("Bank")]
        public int BankId { get; set; }                     // foreign key to bank table
        public virtual Bank? Bank { get; set; }             // navigation property for bank info

        [Required(ErrorMessage = "Balance in Required!")]
        [DataType(DataType.Currency)]
        public double Balance { get; set; } = 0;            // current account balance (default 0)

        [Required(ErrorMessage = "Account Type is Required!")]
        [ForeignKey("AccountType")]
        public int AccountTypeId { get; set; }              // foreign key for account type
        public virtual AccountType? AccountType { get; set; } // navigation property for type details

        [Required(ErrorMessage = "Account Status is Required!")]
        [ForeignKey("AccountStatus")]
        public int AccountStatusId { get; set; }            // foreign key for account status
        public virtual AccountStatus? AccountStatus { get; set; } // navigation property for status

        [Required(ErrorMessage = "Account Creation Date is Required!")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now.Date; // date when account was created

        public virtual List<int>? TransactionIds { get; set; } = new List<int>(); // holds ids of related transactions
    }
    }
