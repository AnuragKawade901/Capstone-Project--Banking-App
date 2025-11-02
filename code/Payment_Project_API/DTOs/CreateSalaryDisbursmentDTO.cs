using Payment_Project_API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment_Project_API.DTOs
{
    public class CreateSalaryDisbursmentDTO
    {
        public int ClientId { get; set; }
        public bool AllEmployees { get; set; } = true;
        public virtual ICollection<int>? EmployeeIds { get; set; } = new List<int>();   

    }
}
