using System.ComponentModel.DataAnnotations;

namespace Payment_Project_API.Models.Enums
{
    public enum TxnType { CREDIT, DEBIT}
    public class TransactionType    
    {
        [Key]
        public int TypeId { get; set; }
        [Required(ErrorMessage = "Type is Required!")]
        public TxnType Type { get; set; }
    }
}
