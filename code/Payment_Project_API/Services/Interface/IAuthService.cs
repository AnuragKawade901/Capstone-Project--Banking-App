using Payment_Project_API.DTOs;

namespace Payment_Project_API.Services.Interface
{
    public interface IAuthService
    {
        public LoginResponseDTO Login(LoginDTO usr);
    }
}
