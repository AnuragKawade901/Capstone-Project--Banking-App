using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.Models.Enums
{
    public enum AccStatus { ACTIVE, INACTIVE, CLOSED }
    public class AccountStatus
    {
        [Key]
        public int StatusId { get; set; }
        [Required(ErrorMessage = "Status is Required!")]
        public AccStatus Status { get; set; }
    }
}
