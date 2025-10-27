using System.ComponentModel.DataAnnotations;

namespace Payment_Project_AP.Models.Enitites
{
    public class ApprovalWorkflow // defines a template for a multi-step approval process.
                                  // this can be configured per client for different types of operations.
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // eg Standard payment approval

        [MaxLength(255)]
        public string Description { get; set; }


        /// the name of the entity this workflow applies to --  eg payment.
        /// this makes the workflow system generic and reusable.
        [Required]
        [MaxLength(50)]
        public string AppliesToEntity { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        //relationship
        // the corporate client this workflow belongs to
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        // the ordered steps that make up this workflow
        public ICollection<ApprovalStep> Steps { get; set; }

        public ApprovalWorkflow()
        {
            Steps = new HashSet<ApprovalStep>();
        }
    }
}
