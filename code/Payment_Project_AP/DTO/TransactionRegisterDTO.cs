using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.DTO
{
    public class TransactionRegisterDTO
    {
        public int AccountId { get; set; }
        public int PayementId { get; set; }
        public int TransactionTypeId { get; set; }
        [Required(ErrorMessage = "Transaction Amount is Required!")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
    }
}
