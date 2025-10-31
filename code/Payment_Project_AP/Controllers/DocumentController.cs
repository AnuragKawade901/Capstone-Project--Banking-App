using AutoMapper;
using Payment_Project_AP.DTO;
using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Models.Enums;
using Payment_Project_AP.Service.Interface;
using Payment_Project_AP.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Payment_Project_AP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IAccountService _accountService;
        private readonly IClientService _clientUserService;
        private readonly ILogger<DocumentController> _logger;
        private readonly IMapper _mapper;

        public DocumentController(IDocumentService documentService, IMapper mapper, IAccountService accountService, IClientService clientUserService, ILogger<DocumentController> logger)
        {
            _documentService = documentService;
            _mapper = mapper;
            _accountService = accountService;
            _clientUserService = clientUserService;
            _logger = logger;
        }

        // GET: api/Document
        [HttpGet]
        public async Task<ActionResult<PagedResultDTO<DocumentDTO>>> GetAll(
            [FromQuery] string? documentName,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            _logger.LogInformation("GetAllDocuments started!");

            var response = await _documentService.GetAll(documentName, pageNumber, pageSize);

            if (!response.Any())
                return NotFound("No documents found!");

            return Ok(response);
        }

        // GET: api/Document/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDTO>> GetById(int id)
        {
            var doc = await _documentService.GetById(id);
            if (doc == null) return NotFound($"Document with ID {id} not found");

            return Ok(doc);
        }

        // POST: api/Document
        [HttpPost]
        public async Task<ActionResult<DocumentDTO>> Add(DocumentDTO dto)
        {
            var document = _mapper.Map<Document>(dto);
            var created = await _documentService.Add(document);
            var createdDto = _mapper.Map<DocumentDTO>(created);

            return CreatedAtAction(nameof(GetById), new { id = created.DocumentId }, createdDto);
        }

        // PUT: api/Document/update/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromForm] DocumentDTO dto)
        {
            var existingDoc = await _documentService.GetById(id);
            if (existingDoc == null)
                return NotFound($"Document with ID {id} not found");

            existingDoc.DocumentName = dto.DocumentName ?? existingDoc.DocumentName;
            existingDoc.DocumentURL = dto.DocumentURL ?? existingDoc.DocumentURL;
            existingDoc.ProofTypeId = dto.ProofTypeId != 0 ? dto.ProofTypeId : existingDoc.ProofTypeId;

            var updatedDoc = await _documentService.Update(existingDoc);

            var updatedDto = new DocumentDTO
            {
                DocumentId = updatedDoc.DocumentId,
                ClientId = updatedDoc.ClientId,
                DocumentName = updatedDoc.DocumentName,
                DocumentURL = updatedDoc.DocumentURL,
                ProofTypeId = updatedDoc.ProofTypeId,
                PublicId = updatedDoc.PublicId
            };

            return Ok(updatedDto);
        }

        // DELETE: api/Document/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingDoc = await _documentService.GetById(id);
            if (existingDoc == null) return NotFound($"Document with ID {id} not found");

            await _documentService.DeleteById(id);
            return NoContent();
        }

        // POST: api/Document/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] DocumentDTO dto)
        {
            _logger.LogInformation("UploadDocument started!");

            if (string.IsNullOrEmpty(dto.DocumentURL))
                return BadRequest("Document URL is required!");

            // Check if client exists
            var client = await _clientUserService.GetById(dto.ClientId);
            if (client == null)
                return NotFound($"Client with id {dto.ClientId} not found!");

            // Save in database
            var document = new Document
            {
                DocumentURL = dto.DocumentURL,
                DocumentName = dto.DocumentName ?? "Document",
                ProofTypeId = dto.ProofTypeId,
                PublicId = dto.PublicId,
                ClientId = dto.ClientId
            };

            Document addedDocument = await _documentService.Add(document);

            // Assign DocumentId to Client
            client.Documents.Add(addedDocument);

            // Update client in DB
            await _clientUserService.Update(client);
            _logger.LogInformation("Document was uploaded!");

            return Ok(new
            {
                DocumentId = addedDocument.DocumentId,
                ClientId = addedDocument.ClientId,
                DocumentURL = addedDocument.DocumentURL,
                PublicId = addedDocument.PublicId,
                Message = "Document saved successfully!"
            });
        }

        // GET: api/Document/client/{clientId}
        [HttpGet("client/{clientId}")]
        public async Task<ActionResult> GetDocumentsByClientId(int clientId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            _logger.LogInformation("GetDocumentsByClientId started!");

            var pagedResult = await _documentService.GetDocumentByClientId(clientId, pageNumber, pageSize);

            if (!pagedResult.Data.Any())
                return NotFound($"No documents found for ClientId {clientId}");

            var docDtos = pagedResult.Data.Select(d => new DocumentDTO
            {
                DocumentId = d.DocumentId,
                ClientId = d.ClientId,
                DocumentName = d.DocumentName,
                DocumentURL = d.DocumentURL,
                ProofTypeId = d.ProofTypeId,
                PublicId = d.PublicId
            }).ToList();

            return Ok(new
            {
                Data = docDtos,
                pagedResult.TotalRecords,
                pagedResult.PageNumber,
                pagedResult.PageSize
            });
        }
    }
}