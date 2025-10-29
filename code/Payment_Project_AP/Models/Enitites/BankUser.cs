using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.Models.Enitites
{
    public class BankUser : User
    {
        [Required(ErrorMessage = "Refferal code is Required!")]
        public string RefferalCode { get; set; }
        [Required(ErrorMessage = "branch is Required!")]
        public string Branch { get; set; }
        public bool isActive { get; set; } = false;
        public IEnumerable<Client> Clients { get; set; } = new List<Client>();
    }
}
