using Eplango.Data;
using Eplango.Dtos;
using Eplango.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Eplango.enums;
using System.Data.Entity.Core.Metadata.Edm;
using System.Security.Cryptography;

namespace Eplango.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        readonly DataContext _context;
        public DepartmentRepository()
        {
            _context = new DataContext();

        }

        public void CreateDepartment(Department department)
        {
            if (department != null)
            {
                _context.Departments.Add(department);
                var newManager = _context.Employees.Find(department.ManagerId);
                newManager.EmployeeType = EmployeeType.ManagerEmployee;
                newManager.DepartmentId = department.DepartmentId;
                _context.SaveChanges();
            }
        }

        public List<DepartmentForListDto> GetDepartments()
        {

            var depts = _context.Departments.Include(e => e.Employees)
                          .Select(dept => new DepartmentForListDto
                          {
                              DepartmentId = dept.DepartmentId,
                              DepartmentName = dept.Name,
                              ManagerName = dept.Employees
              .FirstOrDefault(e => e.EmployeeType == EmployeeType.ManagerEmployee).Name
                    ?? "No Manager"
                          }).ToList();






            if (depts != null)
                return depts;
            return new List<DepartmentForListDto>();
        }

        public async Task<List<Department>> GetDepartmentsForEmployees()
        {
            var depts = await _context.Departments.ToListAsync();
            if (depts != null)
                return depts;
            return new List<Department>();
        }

        public void Delete(Department department)
        {
            if (department != null)
            {
                var prevManager = _context.Employees.Find(department?.ManagerId);
                if (prevManager != null)
                {
                    prevManager.EmployeeType = EmployeeType.NormalEmployee;
                    prevManager.DepartmentId = null;
                }
                _context.Departments.Remove(department);

                _context.SaveChanges();
            }
        }

        public async Task<Department> GetDepartment(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept != null)
                return dept;
            return new Department();

        }

        public void UpdateDepartment(Department department)
        {
            if (department != null)
            {
                var dept = _context.Departments.Find(department.DepartmentId);
                dept.ManagerId = department.ManagerId;
                dept.Name = department.Name;
                _context.SaveChanges();
            }
        }

        public async Task<DepartmentDetailsDto> GetDepartmentDetails(int id)
        {
            var dept = await _context.Departments.Include(e => e.Employees)
                .SingleOrDefaultAsync(d => d.DepartmentId == id);

                var manager = dept.Employees
                    .FirstOrDefault(x => x.EmployeeType ==
                    EmployeeType.ManagerEmployee) ?? new Employee();

         
            



            DepartmentDetailsDto departmentDetailsDto = new DepartmentDetailsDto
            {
                Employees = _context.Employees
                        .Where(e => e.EmployeeType == EmployeeType.NormalEmployee &&
                       e.DepartmentId == id).ToList(),
                DepartManager = manager.Name ?? "No manager",
                DepartmentName = dept.Name
            };
            if (departmentDetailsDto != null)
                return departmentDetailsDto;
            return new DepartmentDetailsDto();

        }
    }
}