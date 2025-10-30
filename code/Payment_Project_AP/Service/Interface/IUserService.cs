using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Service.Interface
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
