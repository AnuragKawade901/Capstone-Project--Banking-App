using Payment_Project_API.DTOs;
using Payment_Project_API.Models;

namespace Payment_Project_API.Repositories.Interface
{
    public interface IAuthRepository
    {
        public LoginResponseDTO Login(LoginDTO usr);
        public bool VerifyPassword(User user, string password);
    }
}
