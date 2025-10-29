namespace Payment_Project_AP.Repositories
{
    public interface IEmailRepository
    {
        public Task SendEmailToClientAsync(int id, string subject, string body);
    }
}
