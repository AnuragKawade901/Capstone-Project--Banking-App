using Payment_Project_API.Data;
using Payment_Project_API.DTOs;
using Payment_Project_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Payment_Project_API.Repositories.Interface;

namespace Payment_Project_API.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly BankingPaymentsDBContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AuthRepository(BankingPaymentsDBContext dBContext,IPasswordHasher<User> passwordHasher) {
            _dbContext = dBContext;
            _passwordHasher = passwordHasher;
        }
        public LoginResponseDTO Login(LoginDTO usr)
        {
            var user = _dbContext.Users.Include(u => u.Role).FirstOrDefault((u) => (u.UserName.Equals(usr.UserName)));

            bool isPasswordCorrect = VerifyPassword(user, usr.Password);
            LoginResponseDTO response;
            if (isPasswordCorrect)
            {
                response = new LoginResponseDTO
                {
                    IsSuccess = true,
                    User = user,
                    Token = ""
                };
                return response;
            }
            response = new LoginResponseDTO
            {
                IsSuccess = false,
                User = null,
                Token = ""

            };
            return response;
        }

        public bool VerifyPassword(User user,string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user,user.Password,password);
            if (result == PasswordVerificationResult.Success) return true;
            return false;
        }
    }
}
