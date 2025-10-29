using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.Models.Enums
{
    public enum TransType { CREDIT, DEBIT }
    public class TransactionType
    {
        [Key]
        public int TypeId { get; set; }
        [Required(ErrorMessage = "Type is Required!")]
        public TransType Type { get; set; }
    }
}
