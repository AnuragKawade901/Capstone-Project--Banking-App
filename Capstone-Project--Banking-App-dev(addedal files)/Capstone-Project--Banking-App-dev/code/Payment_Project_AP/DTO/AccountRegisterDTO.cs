namespace Payment_Project_AP.DTO
{
    public class AccountRegisterDTO
    {
        public string? AccountNumber { get; set; }
        public int ClientId { get; set; }
        public int BankId { get; set; }
        public double Balance { get; set; } = 0;
        public int AccountTypeId { get; set; }
        public int AccountStatusId { get; set; }
    }
}
