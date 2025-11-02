using Payment_Project_API.DTOs;
using Payment_Project_API.Repositories.Interface;
using Payment_Project_API.Services.Interface;

namespace Payment_Project_API.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly ICloudinaryRepository _cloudinaryRepository;

        public CloudinaryService(ICloudinaryRepository cloudinaryRepository)
        {
            _cloudinaryRepository = cloudinaryRepository;
        }

        public async Task<UploadResultDTO> UploadFileAsync(IFormFile file)
        {
            return await _cloudinaryRepository.UploadFileAsync(file);
        }
    }
}
