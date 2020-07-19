using Eplango.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Eplango.Repository
{
    public interface IEmployeeRepostiory
    {
        void CreateEmployee(Employee employee);
        Task<Employee> GetEmployee(int id);
        Task<List<Employee>> GetEmployees();
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Task<Employee> GetManager(int? id);

    }
}