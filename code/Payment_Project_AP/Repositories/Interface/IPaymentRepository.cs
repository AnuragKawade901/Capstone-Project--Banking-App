using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories.Interface
{
    public interface IPaymentRepository
    {
        public IQueryable<Payment> GetAll();
        public Task<Payment> Add(Payment payment);
        public Task<Payment?> GetById(int id);
        public Task<Payment?> Update(Payment payment);
        public Task DeleteById(int id);
    }
}
