using Payment_Project_AP.DTO;
using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Service.Interface
{
    public interface ITransactionService
    {
        public Task<IEnumerable<TransactionDTO>> GetAll(
            int? clientId,
            int? bankuserId,
            int? transactionId,
            int? transactionTypeId,
            DateTime? createdFrom,
            DateTime? createdTo,
            double? minAmount,
            double? maxAmount,
            string? toFrom,
            int? pageNumber,
            int? pageSize);

        public Task<Transaction> Add(Transaction transaction);
        public Task<IEnumerable<Transaction>> AddMany(IEnumerable<Transaction> transactions);
        public Task<Transaction?> GetById(int id);
        public Task<Transaction?> Update(Transaction transaction);
        public Task DeleteById(int id);
    }
}
