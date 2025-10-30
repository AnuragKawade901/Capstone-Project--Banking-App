using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Repositories.Interface
{
    public interface IBeneficiaryRepository
    {
        public IQueryable<Beneficiary> GetAll();
        public Task<Beneficiary> Add(Beneficiary beneficiary);
        public Task<Beneficiary?> GetById(int id);
        public Task<Beneficiary?> Update(Beneficiary beneficiary);
        public Task DeleteById(int id);
    }
}
