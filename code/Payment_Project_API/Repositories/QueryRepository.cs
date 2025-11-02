using Payment_Project_API.Data;
using Payment_Project_API.Models;
using Microsoft.EntityFrameworkCore;
using Payment_Project_API.Repositories.Interface;

namespace Payment_Project_API.Repositories
{
    public class QueryRepository : IQueryRepository
    {
        private readonly BankingPaymentsDBContext _dbContext;

        public QueryRepository(BankingPaymentsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Query> GetAll()
        {
            return _dbContext.Queries.AsQueryable();
        }

        public async Task<Query?> GetById(int id)
        {
            return await _dbContext.Queries.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Query> Add(Query query)
        {
            await _dbContext.Queries.AddAsync(query);
            await _dbContext.SaveChangesAsync();
            return query;
        }
        public async Task<Query> Update(Query query)
        {
            _dbContext.Queries.Update(query);
            await _dbContext.SaveChangesAsync();
            return query;
        }
    }
}
