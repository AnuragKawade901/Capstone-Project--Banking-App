using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories.Interface
{
    public interface IUserRepository
    {
        public IQueryable<User> GetAll();
        public Task<User> Add(User user);
        public Task<User?> GetById(int id);
        public Task<User?> Update(User user);
        public Task DeleteById(int id);
    }
}
