using Payment_Project_API.DTOs;
using Payment_Project_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Payment_Project_API.Repositories.Interface;
using Payment_Project_API.Services.Interface;

namespace Payment_Project_API.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<IEnumerable<Document>> GetAll(string? documentName, int? pageNumber,int? pageSize)
        {
            var query = _documentRepository.GetAll();

            if (!string.IsNullOrEmpty(documentName))
                query = query.Where(d => d.DocumentName.Contains(documentName));

            return query;
        }




        public async Task<Document> Add(Document document)
        {
            return await _documentRepository.Add(document);
        }

        public async Task<Document?> GetById(int id)
        {
            return await _documentRepository.GetById(id);
        }

        public async Task<Document?> Update(Document document)
        {
            return await _documentRepository.Update(document);
        }

        public async Task DeleteById(int id)
        {
            await _documentRepository.DeleteById(id);
        }

        public async Task<PagedResultDTO<Document>> GetDocumentByClientId(int clientId, int pageNumber = 1, int pageSize = 5)
        {
            var query = _documentRepository.GetAll().Where(d => d.ClientId == clientId);

            var totalRecords = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResultDTO<Document>
            {
                Data = data,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }



    }
}
