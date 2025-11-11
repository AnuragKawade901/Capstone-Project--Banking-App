using System.ComponentModel.DataAnnotations;

namespace Payment_Project_API.DTOs
{
    public class BeneficiaryDTO
    {
        public int ClientId { get; set; }
        public string BeneficiaryName { get; set; }
        [Required(ErrorMessage = "AccountNumber is Required!")]
        public string AccountNumber { get; set; }
        [Required(ErrorMessage = "Bank name is Required!")]
        public string BankName { get; set; }
        [Required(ErrorMessage = "IFSC Code is Required!")]
        public string IFSC { get; set; }
    }
}
