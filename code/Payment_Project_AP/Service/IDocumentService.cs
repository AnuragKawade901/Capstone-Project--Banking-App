using Payment_Project_AP.DTO;
using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Service
{
    public interface IDocumentService
    {
        public Task<IEnumerable<Document>> GetAll(
            string? documentName,
            int? pageNumber,
            int? pageSize);
        public Task<Document> Add(Document document);
        public Task<Document?> GetById(int id);
        public Task<Document?> Update(Document document);
        public Task DeleteById(int id);

        Task<PagedResultDTO<Document>> GetDocumentByClientId(
        int clientId,
        int pageNumber = 1,
        int pageSize = 5);
    }
}
