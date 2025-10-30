using Payment_Project_AP.DTO;
using Payment_Project_AP.Models.Enitites;
using Payment_Project_AP.Models.Enums;
using Payment_Project_AP.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Payment_Project_AP.Repositories.Interface;
using Payment_Project_AP.Data;

namespace Payment_Project_AP.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CorporateBankingDBContext _dbContext;
        public EmployeeRepository(CorporateBankingDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public IQueryable<Employee> GetAll()
        {
            return _dbContext.Employees
                             .Include(e => e.Client)
                             .Include(e => e.SalaryPayment)
                             .AsQueryable();
        }

        public async Task<Employee> Add(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> GetById(int id)
        {
            return await _dbContext.Employees.Include(e => e.Client).FirstOrDefaultAsync(e => e.EmployeeId.Equals(id));
        }

        public async Task<Employee?> Update(Employee employee)
        {
            Employee? existingEmployee = await GetById(employee.EmployeeId);

            if (existingEmployee == null)
                return null;

            existingEmployee.EmployeeName = employee.EmployeeName;
            existingEmployee.ClientId = employee.ClientId;
            existingEmployee.AccountNumber = employee.AccountNumber;
            existingEmployee.BankName = employee.BankName;
            existingEmployee.IFSC = employee.IFSC;

            await _dbContext.SaveChangesAsync();
            return existingEmployee;
        }

        public async Task DeleteById(int id)
        {
            Employee? exisitngEmployee = await GetById(id);
            if (exisitngEmployee == null) return;
            exisitngEmployee.IsActive = false;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Employee> inserted, List<string> skipped)> BulkInsert(List<Employee> employees)
        {
            var skipped = new List<string>();
            var inserted = new List<Employee>();

            foreach (var emp in employees)
            {
                // Check duplicate account number
                bool exists = await _dbContext.Employees
                    .AnyAsync(e => e.AccountNumber == emp.AccountNumber);

                if (exists)
                {
                    skipped.Add($"Skipped Employee '{emp.EmployeeName}' - AccountNumber '{emp.AccountNumber}' already exists.");
                    continue;
                }

                inserted.Add(emp);
            }

            if (inserted.Any())
            {
                await _dbContext.Employees.AddRangeAsync(inserted);
                await _dbContext.SaveChangesAsync();
            }

            return (inserted, skipped);
        }



    }
}
