using Payment_Project_API.Models;

namespace Payment_Project_API.Repositories.Interface
{
    public interface IQueryRepository
    {
        IQueryable<Query> GetAll();
        Task<Query?> GetById(int id);
        Task<Query> Add(Query query);
        Task<Query> Update(Query query);
    }
}
