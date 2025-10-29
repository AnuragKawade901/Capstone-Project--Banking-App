namespace Payment_Project_AP.Service
{
    public interface IEmailService
    {
        public Task SendEmailToClientAsync(int id, string subject, string body);
    }
}
