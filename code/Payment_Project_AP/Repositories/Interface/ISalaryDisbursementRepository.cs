using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories.Interface
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
