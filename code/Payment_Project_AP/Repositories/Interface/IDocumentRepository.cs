using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories.Interface
{
    public interface IDocumentRepository
    {
        public IQueryable<Document> GetAll();
        public Task<Document> Add(Document document);
        public Task<Document?> GetById(int id);
        public Task<Document?> Update(Document document);
        public Task DeleteById(int id);
        public Task<IEnumerable<Document>> GetDocumentByClientId(int clientId);
    }
}
