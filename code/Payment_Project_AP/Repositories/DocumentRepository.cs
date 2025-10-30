using Payment_Project_AP.DTO;
using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Models.Enums;
using Payment_Project_AP.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Payment_Project_AP.Repositories.Interface;
using Payment_Project_AP.Data;

namespace Payment_Project_AP.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly CorporateBankingDBContext _dbContext;
        public DocumentRepository(CorporateBankingDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public IQueryable<Document> GetAll()
        {
            return _dbContext.Documents.AsQueryable();
        }


        public async Task<Document> Add(Document document)
        {
            await _dbContext.Documents.AddAsync(document);
            await _dbContext.SaveChangesAsync();
            return document;
        }

        public async Task<Document?> GetById(int id)
        {
            return await _dbContext.Documents.Include(d=>d.DocumentType).FirstOrDefaultAsync(d => d.DocumentId.Equals(id));
        }

        public async Task<Document?> Update(Document document)
        {
            Document? existingDocument = await GetById(document.DocumentId);

            if (existingDocument == null)
                return null;

            existingDocument.DocumentName = document.DocumentName;
            existingDocument.DocumentURL = document.DocumentURL;
            existingDocument.ProofTypeId = document.ProofTypeId;

            await _dbContext.SaveChangesAsync();
            return existingDocument;
        }

        public async Task DeleteById(int id)
        {
            Document? exisitngDocument = await GetById(id);
            if (exisitngDocument == null) return;
            _dbContext.Documents.Remove(exisitngDocument);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Document>> GetDocumentByClientId(int clientId)
        {
            return await _dbContext.Documents
                                   .Include(d => d.DocumentType) 
                                   .Where(d => d.ClientId == clientId)
                                   .ToListAsync();
        }

    }
}
