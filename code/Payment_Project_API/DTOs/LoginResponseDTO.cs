using Payment_Project_API.Models;

namespace Payment_Project_API.DTOs
{
    public class LoginResponseDTO
    {
        public bool IsSuccess { get; set; }
        public User? User { get; set; }
        public string? Token {  get; set; }

    }
}
