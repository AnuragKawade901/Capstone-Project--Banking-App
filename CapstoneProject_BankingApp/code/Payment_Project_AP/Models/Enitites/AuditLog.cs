using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.Models.Enitites
{
    public class AuditLog
    {
        public long Id { get; set; } // using long for high-volume audit tables

        [Required]
        public int UserId { get; set; } // the ID of the user who performed the action

        [Required]
        [MaxLength(50)]
        public string EntityName { get; set; } // the name of the entity that was affected (eg client)

        [Required]
        public string EntityId { get; set; } // the primary key of the affected record

        [Required]
        public AuditType ActionType { get; set; } // the type of action performed (Create, Update, Delete)

        public string OldValues { get; set; } // the state of the data before the change (serialized as JSON)

        public string NewValues { get; set; } // the state of the data after the change (serialized as JSON)

        [MaxLength(50)]
        public string IpAddress { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        //relationships
        public User User { get; set; }
    } }

