using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment_Project_AP.Models.Enitites
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }                         // unique id for each customer

        [Required(ErrorMessage = "User reference is required!")]
        [ForeignKey("User")]
        public int UserId { get; set; }                             // linked user id
        public virtual User? User { get; set; }                     // navigation property to user details

        [Required(ErrorMessage = "Bank reference is required!")]
        [ForeignKey("Bank")]
        public int BankId { get; set; }                             // linked bank id
        public virtual Bank? Bank { get; set; }                     // navigation property to bank details

        [Required(ErrorMessage = "Onboarding date is required!")]
        [DataType(DataType.Date)]
        public DateTime OnboardingDate { get; set; } = DateTime.Now.Date; // date when customer was onboarded

        [Required(ErrorMessage = "Verification status is required!")]
        [ForeignKey("VerificationStatus")]
        public int VerificationStatusId { get; set; }               // foreign key for verification status (e.g., pending, verified)
        public virtual VerificationStatus? VerificationStatus { get; set; } // navigation property for verification status

        public bool IsActive { get; set; } = true;                  // indicates if customer account is active

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;     // record creation timestamp

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }                    // record last updated timestamp
    }
}
