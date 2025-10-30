using Payment_Project_AP.DTO;
using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Models.Enums;
using Payment_Project_AP.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Payment_Project_AP.Repositories.Interface;
using Payment_Project_AP.Data;

namespace BankingPaymentsApp_API.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly CorporateBankingDBContext _dbContext;
        public PaymentRepository(CorporateBankingDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public IQueryable<Payment> GetAll()
        {
            return _dbContext.Payments
                             .Include(p => p.PayerAccount).ThenInclude(a => a.Client)
                             .Include(p => p.PaymentStatus)
                             .Include(p => p.Transactions)
                             .AsQueryable();
        }

        public async Task<Payment> Add(Payment payment)
        {
            await _dbContext.Payments.AddAsync(payment);
            await _dbContext.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment?> GetById(int id)
        {
            return await _dbContext.Payments.Include(p => p.PayerAccount).ThenInclude(a => a.Client).Include(p=>p.PaymentStatus).FirstOrDefaultAsync(p => p.PaymentId.Equals(id));
        }

        public async Task<Payment?> Update(Payment payment)
        {
            Payment? existingPayment = await GetById(payment.PaymentId);

            if (existingPayment == null)
                return null;

            existingPayment.PayerAccountId = payment.PayerAccountId;
            existingPayment.PayeeAccountNumber = payment.PayeeAccountNumber;
            existingPayment.Amount = payment.Amount;
            existingPayment.PaymentStatusId = payment.PaymentStatusId;

            await _dbContext.SaveChangesAsync();
            return existingPayment;
        }

        public async Task DeleteById(int id)
        {
            Payment? exisitngPayment = await GetById(id);
            if (exisitngPayment == null) return;
            _dbContext.Payments.Remove(exisitngPayment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
