using Payment_Project_API.Models;

namespace Payment_Project_API.Repositories.Interface
{
    public interface ISalaryDisbursementDetailsRepository
    {
        Task<IEnumerable<SalaryDisbursementDetails>> GetAll();
        Task<SalaryDisbursementDetails?> GetById(int id);
        Task<SalaryDisbursementDetails> Add(SalaryDisbursementDetails detail);
        Task<SalaryDisbursementDetails?> Update(SalaryDisbursementDetails detail);
        Task DeleteById(int id);
    }
}
