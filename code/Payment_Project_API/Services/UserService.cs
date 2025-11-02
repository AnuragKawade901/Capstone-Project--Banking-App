using Payment_Project_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Payment_Project_API.Repositories.Interface;
using Payment_Project_API.Services.Interface;

namespace Payment_Project_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<IEnumerable<User>> GetAll(
            string? fullName,
            string? userName,
            string? email,
            string? phone,
            int? roleId,
            int? bankId,
            DateTime? joiningDateFrom,
            DateTime? joiningDateTo)
        {
            var query = _userRepository.GetAll();
            if (!string.IsNullOrEmpty(fullName))
                query = query.Where(u => u.UserFullName.Contains(fullName));

            if (!string.IsNullOrEmpty(userName))
                query = query.Where(u => u.UserName.Contains(userName));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(u => u.UserEmail.Contains(email));

            if (!string.IsNullOrEmpty(phone))
                query = query.Where(u => u.UserPhone == phone);

            if (roleId.HasValue)
                query = query.Where(u => u.UserRoleId == roleId.Value);

            if (bankId.HasValue)
                query = query.Where(u => u.BankId == bankId.Value);

            if (joiningDateFrom.HasValue)
                query = query.Where(u => u.UserJoiningDate >= joiningDateFrom.Value);

            if (joiningDateTo.HasValue)
                query = query.Where(u => u.UserJoiningDate <= joiningDateTo.Value);

            return await query.ToListAsync();
        }

        public async Task<User> Add(User user)
        {
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            return await _userRepository.Add(user);
        }

        public async Task<User?> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }
        public async Task<User?> Update(User user)
        {
            return await _userRepository.Update(user);
        }
        public async Task DeleteById(int id)
        {
            await _userRepository.DeleteById(id);
        }

    }
}
