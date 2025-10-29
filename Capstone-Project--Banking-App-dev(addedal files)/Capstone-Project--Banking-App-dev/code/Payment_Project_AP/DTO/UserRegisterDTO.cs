using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.DTO
{
    public class UserRegisterDTO
    {
        public int BankId { get; set; }
        public string UserFullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int UserRoleId { get; set; }
        public string UserEmail { get; set; } = null!;
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
        public string UserPhone { get; set; } = null!;
    }
}
