using Payment_Project_AP.DTO;
using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Models.Enums;
using Payment_Project_AP.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Payment_Project_AP.Repositories.Interface;
using Payment_Project_AP.Data;

namespace Payment_Project_AP.Repositories
{
    public class SupportRepository : ISupportRepository
    {
        private readonly CorporateBankingDBContext _dbContext;

        public SupportRepository(CorporateBankingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Support> GetAll()
        {
            return _dbContext.Supports.AsQueryable();
        }

        public async Task<Support?> GetById(int id)
        {
            return await _dbContext.Supports.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Support> Add(Support query)
        {
            await _dbContext.Supports.AddAsync(query);
            await _dbContext.SaveChangesAsync();
            return query;
        }
        public async Task<Support> Update(Support query)
        {
            _dbContext.Supports.Update(query);
            await _dbContext.SaveChangesAsync();
            return query;
        }
    }
}
