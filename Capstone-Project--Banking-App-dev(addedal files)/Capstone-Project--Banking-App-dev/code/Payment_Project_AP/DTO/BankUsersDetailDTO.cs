using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.DTO
{
    public class BankUsersDetailDTO
    {
        public int BankId { get; set; }
        public string BankName { get; set; } = string.Empty;
        public List<BankUser> BankUsers { get; set; } = new List<BankUser>();
        public List<Client> Clients { get; set; } = new List<Client>();
    }
}
