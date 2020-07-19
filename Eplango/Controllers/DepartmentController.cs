using Eplango.Data;
using Eplango.Dtos;
using Eplango.Models;
using Eplango.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Eplango.Controllers
{
    public class DepartmentController : Controller
    {
        readonly DepartmentRepository _departmentRepository;
        readonly EmployeeRepository _employeeRepository;
        DataContext _context;
        public DepartmentController()
        {
            _departmentRepository = new DepartmentRepository();
            _employeeRepository = new EmployeeRepository();
            _context = new DataContext();
        }
        // GET: Department
        public ActionResult Index()
        {
            var depts = _departmentRepository.GetDepartments();

            return View(depts);
        }

        [HttpGet]
        public async Task<ActionResult> DepartmentDetails(int id)
        {
            var department = await _departmentRepository.GetDepartmentDetails(id);
            return View(department);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var normalEmployees = await _employeeRepository.GetNormalEmployees();
            DepartmentForCreateDto departmentForCreateDto = new DepartmentForCreateDto
            {
                NormalEmployees = normalEmployees
            };
            return View(departmentForCreateDto);
        }

        [HttpPost]
        public ActionResult Create(DepartmentForCreateDto departmentForCreateDto)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department
                {
                    Name = departmentForCreateDto.DepartmentName,
                    ManagerId = departmentForCreateDto.SelectedManagerId
                };
                _departmentRepository.CreateDepartment(department);
                return RedirectToAction("index");
            }
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {

            var dept = await _departmentRepository.GetDepartment(id);
            if (dept != null)
            {
                _departmentRepository.Delete(dept);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var dept = await _departmentRepository.GetDepartment(id);
            var normalEmps = await _employeeRepository.GetNormalEmployees();
            DepartmentForCreateDto departmentForCreateDto = new DepartmentForCreateDto
            {
                NormalEmployees = normalEmps,
                DepartmentName = dept.Name,
                DepartmentId = dept.DepartmentId,
                SelectedManagerId = dept.ManagerId
            };
            HttpCookie userIdCookie = new HttpCookie("PreviousManagerId");
            userIdCookie.Value = dept.ManagerId.ToString();
            Response.Cookies.Add(userIdCookie);
            var manager = await _employeeRepository.GetManager(dept.ManagerId);
            departmentForCreateDto.NormalEmployees.Add(manager);
            return View(departmentForCreateDto);
        }
        [HttpPost]
        public ActionResult Edit(DepartmentForCreateDto departmentForCreateDto)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department
                {
                    ManagerId = departmentForCreateDto.SelectedManagerId,
                    Name = departmentForCreateDto.DepartmentName,
                    DepartmentId = departmentForCreateDto.DepartmentId
                };
                _departmentRepository.UpdateDepartment(department);

                if (Request.Cookies["PreviousManagerId"].Value != "")
                {
                    var previousManagerId = Convert.ToInt32(Request.Cookies["PreviousManagerId"].Value);
                    var prevManger = _context.Employees.Find(previousManagerId);
                    prevManger.DepartmentId = null;
                    prevManger.EmployeeType = enums.EmployeeType.NormalEmployee;
                }


                var newManager = _context.Employees.Find(departmentForCreateDto.SelectedManagerId);
                newManager.DepartmentId = departmentForCreateDto.DepartmentId;
                newManager.EmployeeType = enums.EmployeeType.ManagerEmployee;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}