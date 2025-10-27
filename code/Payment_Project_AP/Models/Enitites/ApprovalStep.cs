using Payment_Project_AP.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.Models.Enitites
{
    public class ApprovalStep //represents a single, ordered step within an ApprovalWorkflow, defining who needs to approve and how many approvals are required
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string StepName { get; set; } // eg - maker review, checker approval


        // the sequence of this step in the workflow (e.g., 1, 2, 3...).

        [Required]
        public int StepOrder { get; set; }


        // the user role required to provide approval at this step.

        [Required]
        public UserRole RequiredRole { get; set; }


        // the number of users with the required role that must approve
        //before the process can move to the next step.

        [Required]
        public int NumberOfApproversRequired { get; set; } = 1;

        //relationship
        // the workflow this step belongs to
        [Required]
        public int ApprovalWorkflowId { get; set; }
        public ApprovalWorkflow Workflow { get; set; }
    }
}
