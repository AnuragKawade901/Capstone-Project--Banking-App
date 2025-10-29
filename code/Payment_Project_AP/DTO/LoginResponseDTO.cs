using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.DTO
{
    public class LoginResponseDTO
    {
        public bool IsSuccess { get; set; }
        public User? User { get; set; }
        //public string? Token { get; set; }
    }
}
