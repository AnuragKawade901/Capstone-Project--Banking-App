using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Repositories.Interface;
using Payment_Project_AP.Service.Interface;

namespace Payment_Project_AP.Service
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly IClientRepository _clientUserRepository;

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IClientRepository clientUserRepository)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _clientUserRepository = clientUserRepository;
        }

        public async Task<IEnumerable<Beneficiary>> GetAll(
             int? clientId,
             string? beneficiaryName,
             string? accountNumber,
             string? bankName,
             string? ifsc,
             int? pageNumber,
            int? pageSize)
        {
            var query = _beneficiaryRepository.GetAll();

            if (clientId.HasValue)
                query = query.Where(b => b.ClientId == clientId.Value);

            if (!string.IsNullOrEmpty(beneficiaryName))
                query = query.Where(b => b.BeneficiaryName.Contains(beneficiaryName));

            if (!string.IsNullOrEmpty(accountNumber))
                query = query.Where(b => b.AccountNumber.Contains(accountNumber));

            if (!string.IsNullOrEmpty(bankName))
                query = query.Where(b => b.BankName.Contains(bankName));

            if (!string.IsNullOrEmpty(ifsc))
                query = query.Where(b => b.IFSC.Contains(ifsc));

            return query;
        }


        public async Task<Beneficiary> Add(Beneficiary beneficiary)
        {
            Client? client = await _clientUserRepository.GetById(beneficiary.ClientId);

            if (client == null) throw new NullReferenceException("No client User of id:" + beneficiary.ClientId);

            Beneficiary addedBeneficiary = await _beneficiaryRepository.Add(beneficiary);
            client.Beneficiaries?.Add(addedBeneficiary);
            await _clientUserRepository.Update(client);
            return addedBeneficiary;
        }

        public async Task<Beneficiary?> GetById(int id)
        {
            return await _beneficiaryRepository.GetById(id);
        }

        public async Task<Beneficiary?> Update(Beneficiary beneficiary)
        {
            return await _beneficiaryRepository.Update(beneficiary);
        }

        public async Task DeleteById(int id)
        {
            await _beneficiaryRepository.DeleteById(id);
        }
    }
}
