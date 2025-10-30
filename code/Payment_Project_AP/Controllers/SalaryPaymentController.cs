using Payment_Project_AP.DTO;
using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Models.Enums;
using Payment_Project_AP.Service.Interface;
using Payment_Project_AP.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Payment_Project_AP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryPaymentController : ControllerBase
    {
        private readonly ISalaryPaymentService _service;

        public SalaryPaymentController(ISalaryPaymentService service)
        {
            _service = service;
        }

        // GET: api/SalaryPayment
        [HttpGet]
        [Authorize(Roles = $"{nameof(Role.ADMIN)},{nameof(Role.CLIENT_USER)},{nameof(Role.BANK_USER)}")]
        public async Task<IActionResult> GetAllDetails()
        {
            var details = await _service.GetAll();
            if (!details.Any())
                return NotFound("No Salary Disbursement Details found!");
            return Ok(details);
        }

        // GET: api/SalaryPayment/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(Role.ADMIN)},{nameof(Role.CLIENT_USER)},{nameof(Role.BANK_USER)}")]
        public async Task<IActionResult> GetDetailById(int id)
        {
            SalaryPayment? detail = await _service.GetById(id);
            if (detail == null)
                return NotFound($"No Salary Disbursement Detail found with id: {id}");
            return Ok(detail);
        }

        // POST: api/SalaryPayment
        [HttpPost]
        [Authorize(Roles = $"{nameof(Role.CLIENT_USER)},{nameof(Role.BANK_USER)}")]
        public async Task<IActionResult> CreateDetail([FromBody] SalaryPayment detail)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var addedDetail = await _service.Add(detail);
            if (addedDetail == null)
                return BadRequest("Unable to add Salary Disbursement Detail!");

            return Ok(addedDetail);
        }

        // PUT: api/SalaryPayment/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = $"{nameof(Role.CLIENT_USER)},{nameof(Role.BANK_USER)}")]
        public async Task<IActionResult> UpdateDetail(int id, [FromBody] SalaryPayment detail)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != detail.DetailId)
                return BadRequest("Detail Id does not match!");

            SalaryPayment? existing = await _service.GetById(id);
            existing.SalaryDisbursementId = detail.DetailId;
            existing.Amount = detail.Amount;
            existing.EmployeeId = detail.EmployeeId;
            existing.TransactionId = detail.TransactionId;

            SalaryPayment? updatedDetail = await _service.Update(existing);

            if (updatedDetail == null)
                return NotFound($"No Salary Payment Detail found with id: {id}");

            return Ok(updatedDetail);
        }

        // DELETE: api/SalaryPayment/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{nameof(Role.CLIENT_USER)},{nameof(Role.BANK_USER)}")]
        public async Task<IActionResult> DeleteDetail(int id)
        {
            await _service.DeleteById(id);
            return Ok($"Salary Disbursement Detail with id: {id} deleted successfully.");
        }
    }
}
