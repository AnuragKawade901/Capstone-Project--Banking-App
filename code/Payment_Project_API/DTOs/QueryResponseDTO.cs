using System.ComponentModel.DataAnnotations;

namespace Payment_Project_API.DTOs
{
    public class QueryResponseDTO
    {
        [Required]
        public string ResponseMessage { get; set; }
    }
}
