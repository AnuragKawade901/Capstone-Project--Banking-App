using Payment_Project_API.Models;

namespace Payment_Project_API.DTOs
{
    public class RegisterAccountDTO
    {
        public string? AccountNumber { get; set; }
        public int ClientId { get; set; }
        public int BankId {  get; set; }
        public double Balance { get; set; } = 0;
        public int AccountTypeId { get; set; }
        public int AccountStatusId { get; set; }
    }
}
