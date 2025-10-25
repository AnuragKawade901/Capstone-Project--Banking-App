using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.Models.Enitites
{
    public class Notification //Represents an in-app notification for a user.
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Message { get; set; }

        public bool IsRead { get; set; } = false;

        [Required]
        public NotificationType NotificationType { get; set; }

        // an optional URL to navigate the user to the relevant page
        [MaxLength(255)]
        public string Url { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //relationship
        // the user who receives this notification
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
