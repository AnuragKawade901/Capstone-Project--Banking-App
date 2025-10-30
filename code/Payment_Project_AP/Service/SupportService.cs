using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Repositories.Interface;
using Payment_Project_AP.Service.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Payment_Project_AP.Service
{
    public class SupportService : ISupportService
    {
        private readonly ISupportRepository _queryRepository;
        private readonly IEmailService _emailService;
        public SupportService(ISupportRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }
        public IQueryable<Support> GetAll()
        {
            return _queryRepository.GetAll();
        }
        public async Task<Support?> GetById(int id)
        {
            return await _queryRepository.GetById(id);
        }
        public async Task<Support> Add(Support support)
        {
            return await _queryRepository.Add(support);
        }

    }
}
