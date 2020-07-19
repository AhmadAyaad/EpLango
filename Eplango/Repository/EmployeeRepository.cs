using Eplango.Data;
using Eplango.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Eplango.enums;
namespace Eplango.Repository
{
    public class EmployeeRepository : IEmployeeRepostiory
    {

        readonly DataContext _context;
        public EmployeeRepository()
        {
            _context = new DataContext();
        }
        public void CreateEmployee(Employee employee)
        {
            if (employee != null)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
            }
        }

        public void DeleteEmployee(Employee emp)
        {
            var employee = _context.Employees.Find(emp.EmployeeId);
            if (employee != null)
            {
                if (employee.EmployeeType == EmployeeType.ManagerEmployee)
                {
                    var managerDept = _context.Departments.Find(employee.DepartmentId);
                    managerDept.ManagerId = null;
                }
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var employee = await _context.Employees
                        .Include(e => e.Department).FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee != null)
                return employee;
            return new Employee();
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _context.Employees
                            .Include(emp => emp.Department).ToListAsync();
            if (employees != null)
                return employees;
            return new List<Employee>();
        }

        public void UpdateEmployee(Employee newEmployee)
        {

            if (newEmployee != null)
            {
                var employee = _context.Employees.Find(newEmployee.EmployeeId);
                employee.Address = newEmployee.Address;
                employee.DepartmentId = newEmployee.DepartmentId;
                employee.EmployeeType = newEmployee.EmployeeType;
                employee.Name = newEmployee.Name;
                employee.Phone = newEmployee.Phone;
                _context.SaveChanges();
            }

        }


        public async Task<List<Employee>> GetNormalEmployees()
        {
            var managers = await _context.Employees
                  .Where(emp => emp.EmployeeType == EmployeeType.NormalEmployee)
                  .ToListAsync();
            if (managers != null)
                return managers;
            return new List<Employee>();
        }

        public async Task<Employee> GetManager(int? id)
        {
            var manager = await _context.Employees.FindAsync(id);
            if (manager != null)
                return manager;
            return new Employee();
        }
    }
}