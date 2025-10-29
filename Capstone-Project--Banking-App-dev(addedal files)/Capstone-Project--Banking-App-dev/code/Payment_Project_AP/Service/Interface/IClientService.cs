using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.Service.Interface
{
    public interface IClientService
    {
        public Task<IEnumerable<Client>> GetAll(
            string? fullName,
            string? userName,
            string? email,
            string? phone,
            int? bankId,
            DateTime? dobFrom,
            DateTime? dobTo,
            string? address,
            bool? kycVerified,
            int? bankUserId,
            int? pageNumber,
            int? pageSize);
        public Task<Client> Add(Client user);
        public Task<Client?> GetById(int id);
        public Task<Client?> Update(Client user);
        public Task DeleteById(int id);
        public Task<Client> ApproveClient(Client clientUser);
        public Task RejectClient(Client clientUser, string reason);
        public Task SoftDelete(int id);
    }
}
