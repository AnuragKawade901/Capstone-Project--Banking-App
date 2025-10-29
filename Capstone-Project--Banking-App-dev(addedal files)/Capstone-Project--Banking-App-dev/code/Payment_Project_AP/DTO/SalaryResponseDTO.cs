using Payment_Project_AP.Models.Enitites;

namespace Payment_Project_AP.DTO
{
    public class SalaryResponseDTO
    {
        public int SalaryDisbursementId { get; set; }
        public int ClientId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DisbursementDate { get; set; } = DateTime.UtcNow;
        public int DisbursementStatusId { get; set; } = 3;
        public bool AllEmployees { get; set; } = true;
        public virtual ICollection<SalaryPayment>? DisbursementDetails { get; set; } = new List<SalaryPayment>();
    }
}
