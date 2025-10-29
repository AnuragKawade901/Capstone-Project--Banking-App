//using Payment_Project_AP.Models.Enitites;

//namespace Payment_Project_AP.Service
//{
//    public class AccountService : IAccountService
//    {
//        private readonly IAccountRepository _accountRepository;
//        private readonly ITransactionRepository _transactionRepository;
//        private readonly IEmailService _emailService;

//        public AccountService(IAccountRepository accountRepository, ITransactionRepository transactionRepository, IEmailService emailService)
//        {
//            _accountRepository = accountRepository;
//            _transactionRepository = transactionRepository;
//            _emailService = emailService;
//        }

//        public async Task<PagedResultDTO<Account>> GetAllAsync(
//            string? accountNumber,
//            int? clientId,
//            int? bankId,
//            int? accountTypeId,
//            int? accountStatusId,
//            decimal? minBalance,
//            decimal? maxBalance,
//            DateTime? createdFrom,
//            DateTime? createdTo,
//            int pageNumber = 1,
//            int pageSize = 10)
//        {
//            IQueryable<Account> query = _accountRepository.GetAll();

//            if (!string.IsNullOrWhiteSpace(accountNumber))
//                query = query.Where(a => a.AccountNumber.Contains(accountNumber));
//            if (clientId.HasValue)
//                query = query.Where(a => a.ClientId == clientId.Value);
//            if (bankId.HasValue)
//                query = query.Where(a => a.BankId == bankId.Value);
//            if (accountTypeId.HasValue)
//                query = query.Where(a => a.AccountTypeId == accountTypeId.Value);
//            if (accountStatusId.HasValue)
//                query = query.Where(a => a.AccountStatusId == accountStatusId.Value);
//            if (minBalance.HasValue)
//                query = query.Where(a => a.Balance >= minBalance.Value);
//            if (maxBalance.HasValue)
//                query = query.Where(a => a.Balance <= maxBalance.Value);
//            if (createdFrom.HasValue)
//                query = query.Where(a => a.CreatedAt >= createdFrom.Value);
//            if (createdTo.HasValue)
//                query = query.Where(a => a.CreatedAt <= createdTo.Value);

//            var totalRecords = await query.CountAsync();

//            var data = await query
//                .Skip((pageNumber - 1) * pageSize)
//                .Take(pageSize)
//                .ToListAsync();

//            return new PagedResultDTO<Account>
//            {
//                Data = data,
//                TotalRecords = totalRecords,
//                PageNumber = pageNumber,
//                PageSize = pageSize
//            };
//        }

//        public async Task<Account> AddAsync(Account account)
//        {
//            if (account == null) throw new ArgumentNullException(nameof(account));
//            return await _accountRepository.Add(account);
//        }

//        public async Task<Account?> GetByIdAsync(int id)
//        {
//            if (id <= 0) throw new ArgumentException("Invalid account id", nameof(id));
//            return await _accountRepository.GetById(id);
//        }

//        public async Task<Account?> UpdateAsync(Account account)
//        {
//            if (account == null) throw new ArgumentNullException(nameof(account));
//            return await _accountRepository.Update(account);
//        }

//        public async Task DeleteByIdAsync(int id)
//        {
//            if (id <= 0) throw new ArgumentException("Invalid account id to delete", nameof(id));
//            await _accountRepository.DeleteById(id);
//        }

//        public async Task<string> GenerateAccountNumberAsync()
//        {
//            return await _accountRepository.GenerateAccountNumber();
//        }

//        public async Task<Transaction> CreditAccountAsync(int accountId, decimal amount, int? paymentId, int? disbursementId, string sourceInfo)
//        {
//            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive");

//            var account = await _accountRepository.GetById(accountId)
//                ?? throw new KeyNotFoundException($"Account with id {accountId} not found.");

//            account.Balance += amount;

//            var creditTransaction = new Transaction
//            {
//                TransactionTypeId = 1, // Assuming 1 = Credit
//                AccountId = accountId,
//                Amount = amount,
//                PaymentId = paymentId,
//                SalaryDisbursementId = disbursementId,
//                ToFrom = sourceInfo ?? string.Empty,
//                CreatedAt = DateTime.UtcNow
//            };

//            var addedTransaction = await _transactionRepository.Add(creditTransaction);
//            await _accountRepository.Update(account);

//            string subject = $"Your account {account.AccountNumber} has been credited";
//            string body = $"""
//                      Amount credited: Rs {amount}
//                      Transaction time: {DateTime.UtcNow}
//                      """;

//            await _emailService.SendEmailToClientAsync((int)account.ClientId, subject, body);

//            return addedTransaction;
//        }

//        public async Task<Transaction> DebitAccountAsync(int accountId, decimal amount, int? paymentId, int? disbursementId, string destinationInfo)
//        {
//            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive");

//            var account = await _accountRepository.GetById(accountId)
//                ?? throw new KeyNotFoundException($"Account with id {accountId} not found.");

//            if (account.Balance < amount)
//                throw new InvalidOperationException("Insufficient balance!");

//            account.Balance -= amount;

//            var debitTransaction = new Transaction
//            {
//                TransactionTypeId = 2, // Assuming 2 = Debit
//                AccountId = accountId,
//                Amount = amount,
//                PaymentId = paymentId,
//                SalaryDisbursementId = disbursementId,
//                ToFrom = destinationInfo ?? string.Empty,
//                CreatedAt = DateTime.UtcNow
//            };

//            var addedTransaction = await _transactionRepository.Add(debitTransaction);
//            await _accountRepository.Update(account);

//            string subject = $"Your account {account.AccountNumber} has been debited";
//            string body = $"""
//                      Amount debited: Rs {amount}
//                      Transaction time: {DateTime.UtcNow}
//                      """;

//            await _emailService.SendEmailToClientAsync((int)account.ClientId, subject, body);

//            return addedTransaction;
//        }

//        public async Task<Account?> GetByAccountNumberAsync(string accountNumber)
//        {
//            if (string.IsNullOrWhiteSpace(accountNumber)) return null;
//            var accounts = _accountRepository.GetAll();
//            return accounts.FirstOrDefault(a => a.AccountNumber.Equals(accountNumber));
//        }

//        public async Task<bool?> CheckAccountBalanceAsync(int accountId, decimal amount)
//        {
//            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive");
//            var account = await GetByIdAsync(accountId);
//            if (account == null) return null;
//            return account.Balance >= amount;
//        }
//    }
//}
