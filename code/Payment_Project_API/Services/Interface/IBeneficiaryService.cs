using Payment_Project_API.DTOs;
using Payment_Project_API.Models;

namespace Payment_Project_API.Services.Interface
{
    public interface IBeneficiaryService
    {
        Task<IEnumerable<Beneficiary>> GetAll(
            int? clientId,
            string? beneficiaryName,
            string? accountNumber,
            string? bankName,
            string? ifsc,
            int? pageNumber,
            int? pageSize);
        public Task<Beneficiary> Add(Beneficiary beneficiary);
        public Task<Beneficiary?> GetById(int id);
        public Task<Beneficiary?> Update(Beneficiary beneficiary);
        public Task DeleteById(int id);
    }
}
