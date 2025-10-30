using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "UserName is Required!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //public string RecaptchaToken { get; set; } = string.Empty;
    }
}
