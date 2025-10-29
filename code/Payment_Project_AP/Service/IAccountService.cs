//using Payment_Project_AP.Models.Enitites;

//namespace Payment_Project_AP.Service
//{
//    /// <summary>
//    /// Service interface to manage banking accounts with business operations.
//    /// </summary>
//    public interface IAccountService
//    {
//        /// <summary>
//        /// Retrieves paged accounts matching filters.
//        /// </summary>
//        Task<PagedResultDTO<Account>> GetAllAsync(
//            string? accountNumber = null,
//            int? clientId = null,
//            int? bankId = null,
//            int? accountTypeId = null,
//            int? accountStatusId = null,
//            decimal? minBalance = null,
//            decimal? maxBalance = null,
//            DateTime? createdFrom = null,
//            DateTime? createdTo = null,
//            int pageNumber = 1,
//            int pageSize = 10,
//            CancellationToken cancellationToken = default);

//        /// <summary>Adds a new account.</summary>
//        Task<Account> AddAsync(Account account, CancellationToken cancellationToken = default);

//        /// <summary>Finds existing account by Id.</summary>
//        Task<Account?> GetByIdAsync(int accountId, CancellationToken cancellationToken = default);

//        /// <summary>Updates account details.</summary>
//        Task<Account?> UpdateAsync(Account account, CancellationToken cancellationToken = default);

//        /// <summary>Deletes account by Id.</summary>
//        Task DeleteByIdAsync(int accountId, CancellationToken cancellationToken = default);

//        /// <summary>Credits an amount to an account, with optional payment/disbursement refs.</summary>
//        Task<Transaction> CreditAccountAsync(int accountId, decimal amount, int? paymentId, int? disbursementId, string sourceInfo, CancellationToken cancellationToken = default);

//        /// <summary>Debits an amount from an account, with optional payment/disbursement refs.</summary>
//        Task<Transaction> DebitAccountAsync(int accountId, decimal amount, int? paymentId, int? disbursementId, string destinationInfo, CancellationToken cancellationToken = default);

//        /// <summary>Generates a valid new unique account number.</summary>
//        Task<string> GenerateAccountNumberAsync(CancellationToken cancellationToken = default);

//        /// <summary>Checks account existence by account number.</summary>
//        Task<Account?> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken = default);

//        /// <summary>Checks if account balance suffices for a given amount.</summary>
//        Task<bool> HasSufficientBalanceAsync(int accountId, decimal amount, CancellationToken cancellationToken = default);
//    }
//}
