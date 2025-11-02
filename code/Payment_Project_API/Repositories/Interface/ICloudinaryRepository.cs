using Payment_Project_API.DTOs;

namespace Payment_Project_API.Repositories.Interface
{
    public interface ICloudinaryRepository
    {
        Task<UploadResultDTO> UploadFileAsync(IFormFile file);
    }
}
