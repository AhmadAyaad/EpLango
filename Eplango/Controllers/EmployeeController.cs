using Eplango.Dtos;
using Eplango.Models;
using Eplango.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Eplango.enums;

namespace Eplango.Controllers
{
    public class EmployeeController : Controller
    {
        readonly EmployeeRepository _employeeRepository;
        readonly DepartmentRepository _departmentRepository;

        public EmployeeController()
        {
            _employeeRepository = new EmployeeRepository();
            _departmentRepository = new DepartmentRepository();
        }

        // GET: Employee
        public async Task<ActionResult> Index()
        {
            var employees = await _employeeRepository.GetEmployees();
            return View(employees);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var depts = await _departmentRepository.GetDepartmentsForEmployees();
            EmployeeForCreateDto employeeForCreateDto = new EmployeeForCreateDto
            {
                Departments = depts
            };
            return View(employeeForCreateDto);
        }
        [HttpPost]
        public ActionResult Create(EmployeeForCreateDto employeeForCreateDto)
        {

            ///server side validation
            if (ModelState.IsValid)
            {
                Employee employee = new Employee()
                {
                    EmployeeType = EmployeeType.NormalEmployee,
                    Address = employeeForCreateDto.Employee.Address,
                    DepartmentId = employeeForCreateDto.SelectedDepartmentId,
                    Name = employeeForCreateDto.Employee.Name,
                    Phone = employeeForCreateDto.Employee.Phone
                };

                _employeeRepository.CreateEmployee(employee);
                return RedirectToAction("index");
            }
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var depts = await _departmentRepository.GetDepartmentsForEmployees();
            var emp = await _employeeRepository.GetEmployee(id);
            EmployeeForCreateDto employeeForCreateDto = new EmployeeForCreateDto
            {
                Departments = depts,
                Employee = emp,
                SelectedDepartmentId = (int)emp.DepartmentId
            };
            return View(employeeForCreateDto);
        }


        [HttpPost]
        public ActionResult Update(EmployeeForCreateDto employeeForCreateDto)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    Address = employeeForCreateDto.Employee.Address,
                    DepartmentId = employeeForCreateDto.SelectedDepartmentId,
                    EmployeeType = employeeForCreateDto.Employee.EmployeeType,
                    Name = employeeForCreateDto.Employee.Name,
                    Phone = employeeForCreateDto.Employee.Phone,
                    EmployeeId = employeeForCreateDto.Employee.EmployeeId
                };
                _employeeRepository.UpdateEmployee(employee);

                return RedirectToAction("index");
            }
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var employee = await _employeeRepository.GetEmployee(id);
            if (employee != null)
            {
                _employeeRepository.DeleteEmployee(employee);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<ActionResult> EmployeeDetails(int id)
        {
            var emp = await _employeeRepository.GetEmployee(id);
            return View(emp);
        }
    }
}