using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment_Project_AP.Models.Enitites
{
    public class User
    {
    [Key]
    public int UserId { get; set; }                              // unique id for each user

    [Required(ErrorMessage = "User full name is required!")]
    [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters.")]
    public string UserFullName { get; set; } = null!;            // user's complete name

    [Required(ErrorMessage = "Username is required!")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
    public string UserName { get; set; } = null!;                // username used for login

    [Required(ErrorMessage = "Password is required!")]
    [DataType(DataType.Password)]
    [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
    public string Password { get; set; } = null!;                // hashed or plain password (to be hashed later)

    [Required(ErrorMessage = "User role is required!")]
    [ForeignKey("Role")]
    public int UserRoleId { get; set; }                          // foreign key for user role
    public virtual UserRole? Role { get; set; }                  // navigation property for user role

    [Required(ErrorMessage = "User email is required!")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(100)]
    public string UserEmail { get; set; } = null!;               // user's email address

    [Required(ErrorMessage = "User phone is required!")]
    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
    public string UserPhone { get; set; } = null!;               // user's contact number

    [ForeignKey("Bank")]
    public int BankId { get; set; }                              // foreign key to bank entity
    public virtual Bank? Bank { get; set; }                      // navigation property for bank info

    [Required(ErrorMessage = "User joining date is required!")]
    [DataType(DataType.Date)]
    public DateTime UserJoiningDate { get; set; } = DateTime.Now.Date; // date when user joined the system

    public bool IsActive { get; set; } = true;                   // indicates if user account is active

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;      // record creation timestamp

    [DataType(DataType.DateTime)]
    public DateTime? UpdatedAt { get; set; }                     // record update timestamp
}
    }


