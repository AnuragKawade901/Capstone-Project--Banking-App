using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Repositories.Interface;
using Payment_Project_AP.Service.Interface;

namespace Payment_Project_AP.Service
{
    public class SalaryPaymentService : ISalaryPaymentService
    {
        private readonly ISalaryPaymentRepository _repository;

        public SalaryPaymentService(ISalaryPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SalaryPayment>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<SalaryPayment> Add(SalaryPayment detail)
        {
            return await _repository.Add(detail);
        }

        public async Task<SalaryPayment?> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<SalaryPayment?> Update(SalaryPayment details)
        {
            return await _repository.Update(details);
        }

        public async Task DeleteById(int id)
        {
            await _repository.DeleteById(id);
        }
    }
}
