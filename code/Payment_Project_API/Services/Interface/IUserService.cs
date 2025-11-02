using Payment_Project_API.Models;

namespace Payment_Project_API.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll(
            string? fullName,
            string? userName,
            string? email,
            string? phone,
            int? roleId,
            int? bankId,
            DateTime? joiningDateFrom,
            DateTime? joiningDateTo);
        Task<User> Add(User user);
        Task<User?> GetById(int id);
        public Task<User?> Update(User user);
        public Task DeleteById(int id);
    }
}
