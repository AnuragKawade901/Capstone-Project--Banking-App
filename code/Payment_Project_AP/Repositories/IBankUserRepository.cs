using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories
{
    public interface IBankUserRepository
    {
        public IQueryable<BankUser> GetAll();
        Task<BankUser> Add(BankUser bankUser);
        Task<BankUser?> GetById(int id);
        Task<BankUser?> Update(BankUser bankUser);
        Task DeleteById(int id);
    }
}
