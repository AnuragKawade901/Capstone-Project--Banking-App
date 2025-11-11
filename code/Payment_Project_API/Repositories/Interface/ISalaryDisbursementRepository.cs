using Payment_Project_API.Models;

namespace Payment_Project_API.Repositories.Interface
{
    public interface ISalaryDisbursementRepository
    {
        public IQueryable<SalaryDisbursement> GetAll();
        Task<SalaryDisbursement?> GetById(int id);
        Task<SalaryDisbursement> Add(SalaryDisbursement disbursement);
        Task<SalaryDisbursement?> Update(SalaryDisbursement disbursement);
        Task DeleteById(int id);
    }
}
