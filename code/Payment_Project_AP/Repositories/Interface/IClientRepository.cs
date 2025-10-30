using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories.Interface
{
    public interface IClientRepository
    {
        public IQueryable<Client> GetAll();
        public Task<Client> Add(Client user);
        public Task<Client?> GetById(int id);
        public Task<Client?> Update(Client user);
        public Task DeleteById(int id);
    }
}
