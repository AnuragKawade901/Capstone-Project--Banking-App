namespace Payment_Project_AP.Service.Interface
{
    public interface IEmailService
    {
        public Task SendEmailToClientAsync(int id, string subject, string body);
    }
}
