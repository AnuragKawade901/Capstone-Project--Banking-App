using Payment_Project_API.Models;

namespace Payment_Project_API.Repositories.Interface
{
    public interface ITransactionRepository
    {
        public IQueryable<Transaction> GetAll(); 
        public Task<Transaction> Add(Transaction transaction);
        public Task<IEnumerable<Transaction>> AddMany(IEnumerable<Transaction> transactions);
        public Task<Transaction?> GetById(int id);
        public Task<Transaction?> Update(Transaction transaction);
        public Task DeleteById(int id);

    }
}
