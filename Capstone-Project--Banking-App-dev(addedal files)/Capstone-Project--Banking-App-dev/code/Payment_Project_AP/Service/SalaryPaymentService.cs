using Payment_Project_AP.Service.Interface;

namespace Payment_Project_AP.Service
{
    public class SalaryPaymentService : ISalaryPaymentService
    {
        private readonly ISalaryDisbursementDetailsRepository _repository;

        public SalaryPaymentService(ISalaryDisbursementDetailsRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<SalaryPaymentService>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<SalaryPaymentService> Add(SalaryPaymentService detail)
        {
            return await _repository.Add(detail);
        }

        public async Task<SalaryPaymentService?> GetById(int id)
        {
            return await _repository.GetById(id);
        }
        public async Task<SalaryPaymentService?> Update(SalaryPaymentService details)
        {
            return await _repository.Update(details);
        }
        public async Task DeleteById(int id)
        {
            await _repository.DeleteById(id);
        }
    }
}
