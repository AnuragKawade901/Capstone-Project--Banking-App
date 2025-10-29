using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Payment_Project_AP.DTO;
using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Service.Interface;

namespace Payment_Project_AP.Service
{
    public class ClientService : IClientService
    {
        private readonly IClientUserRepository _clientUserRepository;
        private readonly IBankUserService _bankUserService;
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public ClientService(IClientUserRepository clientUserRepository, IAccountService accountService, IMapper mapper, IPasswordHasher<User> passwordHasher, IEmailService emailService, IBankUserService bankUserService)
        {
            _clientUserRepository = clientUserRepository;
            _accountService = accountService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _bankUserService = bankUserService;
        }

        public async Task<IEnumerable<Client>> GetAll(
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
            int? pageSize)
        {
            var query = _clientUserRepository.GetAll();

            if (!string.IsNullOrEmpty(fullName))
                query = query.Where(cu => cu.UserFullName.Contains(fullName));

            if (!string.IsNullOrEmpty(userName))
                query = query.Where(cu => cu.UserName.Contains(userName));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(cu => cu.UserEmail.Contains(email));

            if (!string.IsNullOrEmpty(phone))
                query = query.Where(cu => cu.UserPhone == phone);

            if (bankId.HasValue)
                query = query.Where(cu => cu.BankId == bankId.Value);

            if (dobFrom.HasValue)
                query = query.Where(cu => cu.DateOfBirth >= dobFrom.Value);

            if (dobTo.HasValue)
                query = query.Where(cu => cu.DateOfBirth <= dobTo.Value);

            if (!string.IsNullOrEmpty(address))
                query = query.Where(cu => cu.Address.Contains(address));

            if (kycVerified.HasValue)
                query = query.Where(cu => cu.KycVierified == kycVerified.Value);

            if (bankUserId.HasValue)
                query = query.Where(cu => cu.BankUserId == bankUserId.Value);

            return query;
        }

        public async Task<Client> Add(Client user)
        {
            var query = _clientUserRepository.GetAll();
            if (query.Any(cu => cu.UserEmail == user.UserEmail))
            {
                throw new InvalidOperationException("A Client User with this email already exists!");
            }

            if (query.Any(cu => cu.UserPhone == user.UserPhone))
            {
                throw new InvalidOperationException("A Client User with this phone number already exists!");
            }

            BankUser? bankUser = await _bankUserService.GetRandomBankUser(user.BankId);
            if (bankUser == null) user.BankUserId = null;
            else user.BankUserId = bankUser.UserId;

            user.Password = _passwordHasher.HashPassword(user, user.Password);

            Client addedUser = await _clientUserRepository.Add(user);

            string subject = "Client User awaits your Action";
            string body =
                $"""
                A Client User With Name: {addedUser.UserName} has applied for your bank.
                Please carry out the desired Action.
                """;
            await _emailService.SendEmailToClientAsync(bankUser.UserId, subject, body);

            return addedUser;
        }


        public async Task<Client?> GetById(int id)
        {
            return await _clientUserRepository.GetById(id);
        }

        public async Task<Client?> Update(Client user)
        {
            return await _clientUserRepository.Update(user);
        }

        public async Task DeleteById(int id)
        {
            await _clientUserRepository.DeleteById(id);
        }

        public async Task<Client> ApproveClient(Client clientUser)
        {
            clientUser.KycVierified = true;
            AccountRegisterDTO registerAccount = new AccountRegisterDTO
            {
                AccountNumber = await _accountService.GenerateAccountNumber(),
                AccountStatusId = 1,
                AccountTypeId = 1,
                BankId = clientUser.BankId,
                ClientId = clientUser.UserId,
                Balance = 0,
            };

            Account newAccount = _mapper.Map<Account>(registerAccount);
            Account addedAccount = await _accountService.Add(newAccount);

            clientUser.AccountId = newAccount.AccountId;

            Client? updatedUser = await _clientUserRepository.Update(clientUser);

            if (updatedUser == null) throw new KeyNotFoundException($"Client user with userId: {clientUser.UserId} was Not Found");

            string subject = "Your application is Verified!";
            string body =
                $"""
                Congratulations your application is sucessfully Verfied!
                You are now a KYC VERIFIED USER
                Your ACCOUNT CREDENTIALS ARE:
                Account Number : {addedAccount.AccountNumber}
                IFSC Code : BPS12345
                """;
            await _emailService.SendEmailToClientAsync(clientUser.UserId, subject, body);

            return updatedUser;
        }

        public async Task RejectClient(Client clientUser, string reason)
        {
            string subject = "Appication Reverted back";
            await _emailService.SendEmailToClientAsync(clientUser.UserId, subject, reason);
        }

        public async Task SoftDelete(int id)
        {
            Client? client = await _clientUserRepository.GetById(id);
            if (client == null) throw new KeyNotFoundException("No such User");

            client.KycVierified = false;
            await _clientUserRepository.Update(client);
        }


    }
}
