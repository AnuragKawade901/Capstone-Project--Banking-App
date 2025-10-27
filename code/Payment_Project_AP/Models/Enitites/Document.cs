using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Payment_Project_AP.Models.Enitites
{
    public class Document //Represents metadata for a document uploaded to a cloud storage provider.
    {
            public int Id { get; set; }

            [Required]
            [MaxLength(255)]
            public string FileName { get; set; } // the original name of the file

            [Required]
            public string Url { get; set; } // the secure URL where the file is stored (from Cloudinary)

            [Required]
            public string PublicId { get; set; } // the unique identifier from Cloudinary, needed for file management (e.g., deletion)

            [Required]
            public DocumentType DocumentType { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            // relationship

            // the customer this document belongs to, primarily for onboarding
            [Required]
            public int CustomerId { get; set; }
            public Customer Customer { get; set; }
        }
    }

