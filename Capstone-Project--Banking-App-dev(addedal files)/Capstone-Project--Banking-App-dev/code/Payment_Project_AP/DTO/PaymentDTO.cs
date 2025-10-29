using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.DTO
{
    public class PaymentDTO
    {
        public int PayerAccountId { get; set; }
        public string PayeeAccountNumber { get; set; }
        [Required(ErrorMessage = "Amount is Required!")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
    }
}
