using Payment_Project_API.Models;

namespace Payment_Project_API.DTOs
{
    public class BankUsersPerBankDTO
    {
        public int BankId { get; set; }
        public string BankName { get; set; } = string.Empty;
        public List<BankUser> BankUsers { get; set; } = new List<BankUser>();
        public List<ClientUser> ClientUsers { get; set; } = new List<ClientUser>();
    }
}
