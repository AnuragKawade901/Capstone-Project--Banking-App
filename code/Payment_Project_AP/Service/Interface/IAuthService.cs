using Payment_Project_AP.DTO;

namespace Payment_Project_AP.Service.Interface
{
    public interface IAuthService
    {
        public LoginResponseDTO Login(LoginDTO usr);
    }
}
