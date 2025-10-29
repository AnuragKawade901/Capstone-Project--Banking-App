namespace Payment_Project_AP.DTO
{
    public class CreateSalaryDisbursmentDTO
    {
        public int ClientId { get; set; }
        public bool AllEmployees { get; set; } = true;
        public virtual ICollection<int>? EmployeeIds { get; set; } = new List<int>();
    }
}
