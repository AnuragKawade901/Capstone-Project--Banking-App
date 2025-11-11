using Payment_Project_API.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Payment_Project_API.Repositories.Interface;
using Payment_Project_API.Services.Interface;

namespace Payment_Project_API.Services
{
    public class QueryService : IQueryService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IEmailService _emailService;
        public QueryService(IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }
        public IQueryable<Query> GetAll()
        {
            return _queryRepository.GetAll();
        }
        public async Task<Query?> GetById(int id)
        {
            return await _queryRepository.GetById(id);
        }
        public async Task<Query> Add(Query query)
        {
            return await _queryRepository.Add(query);
        }

    }
}
