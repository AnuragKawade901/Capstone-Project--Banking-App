using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.Models.Enums
{
    public enum AccType { SAVINGS, CURRENT, SALARY }
    public class AccountType
    {
        [Key]
        public int TypeId { get; set; }
        [Required(ErrorMessage = "Type is Required!")]
        public AccType Type { get; set; }
    }
}
