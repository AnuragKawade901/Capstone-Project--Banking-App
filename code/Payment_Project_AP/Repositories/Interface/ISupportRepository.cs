using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories.Interface
{
    public interface ISupportRepository
    {
        IQueryable<Support> GetAll();
        Task<Support?> GetById(int id);
        Task<Support> Add(Support support);
        Task<Support> Update(Support support);
    }
}
