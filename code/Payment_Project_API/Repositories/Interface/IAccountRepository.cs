using Payment_Project_API.Models;

namespace Payment_Project_API.Repositories.Interface
{
    public interface IAccountRepository 
    {
        public IQueryable<Account> GetAll();
        Task<Account> Add(Account account);
        Task<Account?> GetById(int id);
        Task<Account?> Update(Account account);
        Task DeleteById(int id);
        Task<string> GenerateAccountNumber();
    }
}
