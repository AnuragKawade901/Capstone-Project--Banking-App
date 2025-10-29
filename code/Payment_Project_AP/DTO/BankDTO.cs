namespace Payment_Project_AP.DTO
{
    public class BankDTO
    {
        public string BankName { get; set; } = string.Empty;
        public string IFSC { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}