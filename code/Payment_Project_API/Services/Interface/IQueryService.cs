using Payment_Project_API.Models;

namespace Payment_Project_API.Services.Interface
{
    public interface IQueryService
    {
        IQueryable<Query> GetAll();
        Task<Query?> GetById(int id);
        Task<Query> Add(Query query);
    }
}
