
using Microsoft.EntityFrameworkCore;
using Payment_Project_AP.Data;
using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Repositories.Interface;

namespace BankingPaymentsApp_API.Repositories
{
    public class SalaryPaymentRepository : ISalaryPaymentRepository
    {
        private readonly CorporateBankingDBContext _dbContext;

        public SalaryPaymentRepository(CorporateBankingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SalaryPayment>> GetAll()
        {
            return await _dbContext.SalaryPayments
                .Include(d => d.SalaryDisbursement)
                .Include(d => d.Employee)
                .Include(d => d.Transaction)
                .ToListAsync();
        }

        public async Task<SalaryPayment?> GetById(int id)
        {
            return await _dbContext.SalaryPayments
                .Include(d => d.SalaryDisbursement)
                .Include(d => d.Employee)
                .Include(d => d.Transaction)
                .FirstOrDefaultAsync(d => d.DetailId == id);
        }

        public async Task<SalaryPayment> Add(SalaryPayment detail)
        {
            await _dbContext.SalaryPayments.AddAsync(detail);
            await _dbContext.SaveChangesAsync();
            return detail;
        }

        public async Task<SalaryPayment?> Update(SalaryPayment detail)
        {
            var existing = await GetById(detail.DetailId);
            if (existing == null) return null;

            existing.SalaryDisbursementId = detail.SalaryDisbursementId;
            existing.EmployeeId = detail.EmployeeId;
            existing.Amount = detail.Amount;
            existing.TransactionId = detail.TransactionId;
            existing.Success = detail.Success;

            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteById(int id)
        {
            var existing = await GetById(id);
            if (existing == null) return;

            _dbContext.SalaryPayments.Remove(existing);
            await _dbContext.SaveChangesAsync();
        }
    }
}
