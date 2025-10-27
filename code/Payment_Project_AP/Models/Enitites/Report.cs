using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.Models.Enitites
{
    public class Report //represents the metadata for a generated report, such as its type, status, and download link.
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string ReportName { get; set; } //eg "October 2025 Transaction Summary"

        [Required]
        public ReportType ReportType { get; set; }

        public ReportStatus Status { get; set; } = ReportStatus.Pending;

        // stores the parameters used to generate the report (e.g., as a JSON string)
        public string Parameters { get; set; }

        // the URL to the generated file (PDF, CSV, etc.) stored in the cloud
        public string FileUrl { get; set; }

        public ReportFormat Format { get; set; }

        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; } // reports can be set to expire for cleanup

        // relationship
        // the user who requested the report
        [Required]
        public int GeneratedByUserId { get; set; }
        public User GeneratedByUser { get; set; }

        // the client this report is for (can be null for system-wide reports)
        public int? ClientId { get; set; }
        public Client Client { get; set; }
    }
}
