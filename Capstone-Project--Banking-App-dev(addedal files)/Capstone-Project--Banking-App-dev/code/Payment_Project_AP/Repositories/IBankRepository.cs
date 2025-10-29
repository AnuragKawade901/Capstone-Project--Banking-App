using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories
{
    public interface IBankRepository
    {
        public IQueryable<Bank> GetAll();

        public Task<Bank?> GetById(int id);

        public Task<Bank> Add(Bank bank);

        public Task<Bank?> Update(Bank bank);

        public Task DeleteById(int id);
    }
}
