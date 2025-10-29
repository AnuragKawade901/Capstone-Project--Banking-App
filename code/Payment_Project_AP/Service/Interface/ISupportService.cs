using Payment_Project_AP.Models.Enitites;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Payment_Project_AP.Service.Interface
{
    public interface ISupportService
    {
        IQueryable<Support> GetAll();
        Task<Support?> GetById(int id);
        Task<Support> Add(Support support);
    }
}
