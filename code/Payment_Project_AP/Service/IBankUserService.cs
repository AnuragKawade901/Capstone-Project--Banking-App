using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Service
{
    public interface IBankUserService
    {
        Task<IEnumerable<BankUser>> GetAll(
            string? fullName,
            string? userName,
            string? email,
            string? phone,
            int? roleId,
            int? bankId,
            string? branch,
            DateTime? joiningFrom,
            DateTime? joiningTo,
            int? pageNumber,
            int? pageSize);
        Task<BankUser> Add(BankUser bankUser);
        Task<BankUser?> GetById(int id);
        Task<BankUser?> Update(BankUser bankUser);
        Task DeleteById(int id);
        public Task<BankUser?> GetRandomBankUser(int bankId);
        public Task<BankUser> ApproveBankUser(int id);
        public Task<BankUser> RejectBankUser(int id, RejectDTO reject);
    }
}
