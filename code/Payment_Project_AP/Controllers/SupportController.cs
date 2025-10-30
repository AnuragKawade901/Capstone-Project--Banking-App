using Payment_Project_AP.DTO;
using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Models.Enums;
using Payment_Project_AP.Service.Interface;
using Payment_Project_AP.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Payment_Project_AP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly ISupportService _queryService;
        private readonly ILogger<SupportController> _logger;

        public SupportController(ISupportService queryService, ILogger<SupportController> logger)
        {
            _queryService = queryService;
            _logger = logger;
        }

        // GET: api/Support
        [HttpGet]
        public IActionResult GetAllQueries()
        {
            var supports = _queryService.GetAll().ToList();
            if (supports.Count == 0)
                return NotFound("No supports found.");
            return Ok(supports);
        }

        // GET: api/Support/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQueryById(int id)
        {
            var query = await _queryService.GetById(id);
            if (query == null)
                return NotFound($"No query found with id: {id}");
            return Ok(query);
        }

        // POST: api/Support
        [HttpPost]
        public async Task<IActionResult> CreateQuery([FromBody] Support query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addedQuery = await _queryService.Add(query);
            return CreatedAtAction(nameof(GetQueryById), new { id = addedQuery.Id }, addedQuery);
        }

    }
}
