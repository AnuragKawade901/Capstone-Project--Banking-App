using AutoMapper;
using Payment_Project_AP.DTO;
using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Payment_Project_AP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly ILogger<BeneficiaryController> _logger;
        private readonly IMapper _mapper;

        public BeneficiaryController(IBeneficiaryService beneficiaryService, IMapper mapper, ILogger<BeneficiaryController> logger)
        {
            _beneficiaryService = beneficiaryService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBeneficiaries(
             [FromQuery] int? clientId,
             [FromQuery] string? beneficiaryName,
             [FromQuery] string? accountNumber,
             [FromQuery] string? bankName,
             [FromQuery] string? ifsc,
             [FromQuery] int? pageNumber,
             [FromQuery] int? pageSize)
        {
            _logger.LogInformation("GetAllBeneficiaries started!");

            var response = await _beneficiaryService.GetAll(clientId, beneficiaryName, accountNumber, bankName, ifsc, pageNumber, pageSize);

            if (!response.Any())
                return Ok(response);

            _logger.LogInformation($"beneficiaries displayed!");

            return Ok(response);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateBeneficiary(BeneficiaryDTO beneficiary)
        {
            _logger.LogInformation("CreateBeneficiary started!");

            if (!ModelState.IsValid) return BadRequest(ModelState);
            Beneficiary newBeneficiary = _mapper.Map<Beneficiary>(beneficiary);
            Beneficiary addedBeneficiary = await _beneficiaryService.Add(newBeneficiary);

            _logger.LogInformation("Beneficiary was created");
            return CreatedAtAction("CreateBeneficiary", addedBeneficiary);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBeneficiaryById(int id)
        {
            _logger.LogInformation("GetBeneficiaryById started!");

            Beneficiary? existingBeneficiary = await _beneficiaryService.GetById(id);
            if (existingBeneficiary == null)
                return NotFound($"No Beneficiary of id: {id}");

            _logger.LogInformation($"Beneficiary with id {id} was displayed!");
            return Ok(existingBeneficiary);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBeneficiary(int id, BeneficiaryDTO beneficiary)
        {
            _logger.LogInformation("UpdateBeneficiary started!");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Beneficiary? existingBeneficiary = await _beneficiaryService.GetById(id);
            if (existingBeneficiary == null)
                return NotFound($"No Beneficiary of id: {id}");

            _mapper.Map(beneficiary, existingBeneficiary);

            Beneficiary? updatedBeneficiary = await _beneficiaryService.Update(existingBeneficiary);
            _logger.LogInformation("Beneficiary was updated!");

            return Ok(updatedBeneficiary);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBeneficiary(int id)
        {
            _logger.LogInformation("DeleteBeneficiary started!");

            Beneficiary? existingBeneficiary = await _beneficiaryService.GetById(id);
            if (existingBeneficiary == null)
                return NotFound($"No Beneficiary of id: {id}");

            await _beneficiaryService.DeleteById(id);
            _logger.LogInformation("UpdateBeneficiary sucessfull!");

            return Ok("Beneficiary has been deleted Sucessfully!");
        }
    }
}