using Microsoft.IdentityModel.Tokens;
using Payment_Project_AP.DTO;
using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Repositories.Interface;
using Payment_Project_AP.Service.Interface;
using System.Security.Claims;
using System.Text;

namespace Payment_Project_AP.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public LoginResponseDTO Login(LoginDTO usr)
        {
            if (usr == null)
                throw new ArgumentNullException(nameof(usr));

            var response = _authRepository.Login(usr);

            // Simply return the response from repository
            // (No token generation now)
            return response;
        }
    }
}
