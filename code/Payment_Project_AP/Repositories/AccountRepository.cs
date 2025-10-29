using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingPaymentsDBContext _dbContext;
        private static readonly Random _random = new();

        public AccountRepository(BankingPaymentsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Account> GetAll()
        {
            return _dbContext.Accounts
                .Include(a => a.ClientUser)
                .Include(a => a.Bank)
                .Include(a => a.AccountType)
                .Include(a => a.AccountStatus)
                .AsQueryable();
        }

        public async Task<Account> AddAsync(Account account, CancellationToken cancellationToken = default)
        {
            await _dbContext.Accounts.AddAsync(account, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return account;
        }

        public async Task<Account?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Accounts
                .Include(a => a.ClientUser)
                .Include(a => a.AccountStatus)
                .Include(a => a.AccountType)
                .Include(a => a.Bank)
                .FirstOrDefaultAsync(u => u.AccountId == id, cancellationToken);
        }

        public async Task<Account?> UpdateAsync(Account account, CancellationToken cancellationToken = default)
        {
            var existingAccount = await GetByIdAsync(account.AccountId, cancellationToken);
            if (existingAccount == null) return null;

            existingAccount.Balance = account.Balance;
            existingAccount.AccountStatusId = account.AccountStatusId;
            existingAccount.AccountNumber = account.AccountNumber;
            existingAccount.AccountTypeId = account.AccountTypeId;
            existingAccount.ClientId = account.ClientId;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return existingAccount;
        }

        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var existingAccount = await GetByIdAsync(id, cancellationToken);
            if (existingAccount == null) return;

            _dbContext.Accounts.Remove(existingAccount);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<string> GenerateAccountNumberAsync(int maxRetries = 5)
        {
            string prefix = "BPA";
            string datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < maxRetries; i++)
            {
                string randomPart = new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[_random.Next(s.Length)]).ToArray());

                string accountNumber = $"{prefix}{datePart}{randomPart}";

                bool exists = await _dbContext.Accounts.AnyAsync(a => a.AccountNumber == accountNumber);
                if (!exists)
                {
                    return accountNumber;
                }
            }

            throw new InvalidOperationException("Failed to generate a unique account number after multiple attempts.");
        }
    }
}
