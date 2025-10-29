using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.Models.Enums
{
    public enum DocType
    {
        IDENTITY_PROOF,
        ADDRESS_PROOF,
        DATE_OF_BIRTH_PROOF,
        PHOTOGRAPH,
        PAN_CARD,
        OTHER
    }
    public class DocumentType
    {
        [Key]
        public int TypeId { get; set; }
        [Required(ErrorMessage = "Type is Required!")]
        public DocType Type { get; set; }
    }
}
