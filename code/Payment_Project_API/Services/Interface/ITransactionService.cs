using Payment_Project_API.DTOs;
using Payment_Project_API.Models;

namespace Payment_Project_API.Services.Interface
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
