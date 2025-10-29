using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Gets an IQueryable for all accounts to allow flexible querying.
        /// </summary>
        IQueryable<Account> GetAll();

        /// <summary>
        /// Adds a new account entity asynchronously.
        /// </summary>
        Task<Account> AddAsync(Account account);

        /// <summary>
        /// Retrieves an account by its unique identifier asynchronously.
        /// Returns null if not found.
        /// </summary>
        Task<Account?> GetByIdAsync(int id);

        /// <summary>
        /// Updates an existing account asynchronously.
        /// Returns the updated account or null if not found.
        /// </summary>
        Task<Account?> UpdateAsync(Account account);

        /// <summary>
        /// Deletes the account by its unique identifier asynchronously.
        /// </summary>
        Task DeleteByIdAsync(int id);

        /// <summary>
        /// Generates a new, unique account number asynchronously following business rules.
        /// </summary>
        Task<string> GenerateAccountNumberAsync();
    }
}
