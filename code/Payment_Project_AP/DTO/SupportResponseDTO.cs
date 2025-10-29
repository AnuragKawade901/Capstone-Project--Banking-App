using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.DTO
{
    public class SupportResponseDTO
    {
        [Required]
        public string ResponseMessage { get; set; }
    }
}
