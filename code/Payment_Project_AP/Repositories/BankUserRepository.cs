using Microsoft.EntityFrameworkCore;
using Payment_Project_AP.Data;
using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories
{
    public class BankUserRepository : IBankUserRepository
    {
        private readonly CorporateBankingDBContext _dbContext;
        public BankUserRepository(CorporateBankingDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public IQueryable<BankUser> GetAll()
        {
            return _dbContext.BankUsers
                .Include(u => u.Bank)
                .Include(u => u.Role)
                .Include(u => u.Clients).ThenInclude(cu => cu.Bank)
                .AsQueryable();
        }

        public async Task<BankUser> Add(BankUser user)
        {
            await _dbContext.BankUsers.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<BankUser?> GetById(int id)
        {
            return await _dbContext.BankUsers.Include(u => u.Bank).Include(u => u.Role).Include(u => u.Clients).FirstOrDefaultAsync(u => u.UserId.Equals(id));
        }
        public async Task<BankUser?> Update(BankUser user)
        {
            BankUser? existingUser = await GetById(user.UserId);

            if (existingUser == null)
                return null;

            existingUser.UserEmail = user.UserEmail;
            existingUser.UserPhone = user.UserPhone;
            existingUser.UserFullName = user.UserFullName;
            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.Branch = user.Branch;
            existingUser.RefferalCode = user.RefferalCode;

            await _dbContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task DeleteById(int id)
        {
            BankUser? existingUser = await GetById(id);

            if (existingUser == null) return;

            _dbContext.BankUsers.Remove(existingUser);
            await _dbContext.SaveChangesAsync();
        }
    }
}
