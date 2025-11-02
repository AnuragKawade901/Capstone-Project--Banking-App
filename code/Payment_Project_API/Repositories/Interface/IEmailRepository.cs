namespace Payment_Project_API.Repositories.Interface
{
    public interface IEmailRepository
    {
        public Task SendEmailToClientAsync(int id, string subject, string body);
    }
}
