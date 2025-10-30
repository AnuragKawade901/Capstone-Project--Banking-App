using Payment_Project_AP.Models.Enitites;
using Microsoft.EntityFrameworkCore;
using Payment_Project_AP.Data;
using Payment_Project_AP.Repositories.Interface;

namespace Payment_Project_AP.Repositories   
{
    public class ClientRepository : IClientRepository
    {
        private readonly CorporateBankingDBContext _dbContext;
        public ClientRepository(CorporateBankingDBContext dBContext)
        {
            _dbContext = dBContext;
        }


        public IQueryable<Client> GetAll()
        {
            return _dbContext.Clients
                             .Include(cu => cu.Account)
                             .Include(cu => cu.Beneficiaries)
                             .Include(cu => cu.Employees)
                             .Include(cu => cu.BankUser)
                             .Include(cu => cu.Documents)
                             .Include(cu=>cu.Bank)
                             .AsQueryable();
        }

        public async Task<Client> Add(Client user)
        {
            await _dbContext.Clients.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<Client?> GetById(int id)
        {
            return await _dbContext.Clients.Include(u => u.Role).Include(u => u.Account).Include(u => u.Documents).Include(u=>u.Employees).Include(u=>u.Beneficiaries).Include(u=>u.BankUser).Include(u=>u.Bank).FirstOrDefaultAsync(d => d.UserId.Equals(id));
        }

        public async Task<Client?> Update(Client user)
        {
            Client? existingClient = await GetById(user.UserId);

            if (existingClient == null)
                return null;

            existingClient.UserEmail = user.UserEmail;
            existingClient.UserPhone = user.UserPhone;
            existingClient.UserFullName = user.UserFullName;
            existingClient.UserName = user.UserName;
            existingClient.Password = user.Password;
            existingClient.UserRoleId = user.UserRoleId;
            existingClient.Address = user.Address;
            existingClient.DateOfBirth = user.DateOfBirth;
            existingClient.KycVierified = user.KycVierified;
            existingClient.AccountId = user.AccountId;
            existingClient.BankUserId = user.BankUserId;

            await _dbContext.SaveChangesAsync();
            return existingClient;
        }

        public async Task DeleteById(int id)
        {
            Client? existingClient = await GetById(id);
            if (existingClient == null) return;
            _dbContext.Clients.Remove(existingClient);
            await _dbContext.SaveChangesAsync();
        }
    }
}
