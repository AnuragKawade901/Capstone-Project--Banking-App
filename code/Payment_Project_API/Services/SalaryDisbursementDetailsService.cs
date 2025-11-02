using Payment_Project_API.Models;
using Payment_Project_API.Repositories.Interface;
using Payment_Project_API.Services.Interface;

namespace Payment_Project_API.Services
{
    public class SalaryDisbursementDetailsService : ISalaryDisbursementDetailsService
    {
        private readonly ISalaryDisbursementDetailsRepository _repository;

        public SalaryDisbursementDetailsService(ISalaryDisbursementDetailsRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<SalaryDisbursementDetails>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<SalaryDisbursementDetails> Add(SalaryDisbursementDetails detail)
        {
            return await _repository.Add(detail);
        }

        public async Task<SalaryDisbursementDetails?> GetById(int id)
        {
            return await _repository.GetById(id);
        }
        public async Task<SalaryDisbursementDetails?> Update(SalaryDisbursementDetails details)
        {
            return await _repository.Update(details);
        }
        public async Task DeleteById(int id)
        {
            await _repository.DeleteById(id);
        }
    }
}
