namespace Payment_Project_API.Services.Interface
{
    public interface IEmailService
    {
        public Task SendEmailToClientAsync(int id, string subject, string body);
    }
}
