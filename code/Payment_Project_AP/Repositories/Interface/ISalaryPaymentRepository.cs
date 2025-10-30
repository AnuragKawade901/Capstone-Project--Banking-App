using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories.Interface
{
    public interface ISalaryPaymentRepository
    {
        Task<IEnumerable<SalaryPayment>> GetAll();
        Task<SalaryPayment?> GetById(int id);
        Task<SalaryPayment> Add(SalaryPayment detail);
        Task<SalaryPayment?> Update(SalaryPayment detail);
        Task DeleteById(int id);
    }
}
