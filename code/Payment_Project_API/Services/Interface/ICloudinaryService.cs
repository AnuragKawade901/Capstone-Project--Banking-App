using Payment_Project_API.DTOs;

namespace Payment_Project_API.Services.Interface
{
    public interface ICloudinaryService
    {
        Task<UploadResultDTO> UploadFileAsync(IFormFile file);
    }
}
