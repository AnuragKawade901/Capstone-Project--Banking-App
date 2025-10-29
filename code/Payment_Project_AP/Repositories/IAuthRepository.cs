using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories
{
    public interface IAuthRepository
    {
        public LoginResponseDTO Login(LoginDTO usr);
        public bool VerifyPassword(User user, string password);
    }
}
