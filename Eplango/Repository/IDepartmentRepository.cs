using Eplango.Dtos;
using Eplango.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Eplango.Repository
{
    public interface IDepartmentRepository
    {


        void CreateDepartment(Department department);
        Task<Department> GetDepartment(int id);
        Task<DepartmentDetailsDto> GetDepartmentDetails(int id);
        List<DepartmentForListDto> GetDepartments();
        Task<List<Department>> GetDepartmentsForEmployees();
        void UpdateDepartment(Department department);
        void Delete(Department department);
    }
}