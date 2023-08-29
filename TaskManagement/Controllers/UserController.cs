using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.DTO;
using TaskManagement.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IManagerMapper _managerMapper;
        private readonly IDepartmentMapper _departmentMapper;
        private readonly IEmployeeMapper _employeeMapper;

        public UserController(ApplicationDbContext db, IManagerMapper managerMapper, IDepartmentMapper departmentMapper, IEmployeeMapper employeeMapper)
        {
            _db = db;
            _managerMapper = managerMapper;
            _departmentMapper = departmentMapper;
            _employeeMapper = employeeMapper;
        }

        [HttpGet]
        public IActionResult ManagersList()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                var Managers = _db.User.Where(a => a.Role == "Manager").Include(a => a.Department).ToList();
                return View(Managers);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AddManager()
        {
            var DepartmentNames = _db.Department.Select(d => d.DepartmentName).ToList();
            ViewBag.DepartmentNames = DepartmentNames;
            return View();
        }

        [HttpPost]
        public IActionResult AddManager(ManagerDTO managerDTO)
        {
            if (ModelState.IsValid)
            {
                _db.User.Add(_managerMapper.Map(managerDTO));

                _db.SaveChanges();
                TempData["success"] = "A new manager was added successfully";
                return RedirectToAction("ManagersList");
            }

            return View(managerDTO);
        }

        [HttpGet]
        public IActionResult EditManager(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var managerfromdb = _db.User.Find(id);

            if (managerfromdb == null)
            {
                return NotFound();
            }
            var DepartmentNames = _db.Department.Select(d => d.DepartmentName).ToList();
            ViewBag.DepartmentNames = DepartmentNames;
            var assignedDepartmentName = _db.Department.Where(d => d.UserId == managerfromdb.UserId)
                                                       .Select(d=> d.DepartmentName)
                                                       .FirstOrDefault();
            ViewBag.AssignedDepartmentName = assignedDepartmentName;
            
            return View(managerfromdb);
        }

        [HttpPost]
        public IActionResult EditManager(ManagerDTO managerDTO)
        {
            if (ModelState.IsValid)
            {
                _db.User.Update(_managerMapper.Map(managerDTO));
                _db.SaveChanges();
                TempData["success"] = "Manager updated successfully";
                return RedirectToAction("ManagersList");
            }
            return View(managerDTO);
        }

        [HttpGet]
        public IActionResult DeleteManager(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var managerfromdb = _db.User.Find(id);

            if (managerfromdb == null)
            {
                return NotFound();
            }
            var assignedDepartmentName = _db.Department.Where(d => d.UserId == managerfromdb.UserId)
                                                       .Select(d => d.DepartmentName)
                                                       .FirstOrDefault();
            ViewBag.AssignedDepartmentName = assignedDepartmentName;
            return View(managerfromdb);
        }

        [HttpPost, ActionName("DeleteManager")]
        public IActionResult DeleteManagerPOST(int? id)
        {
            var obj = _db.User.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.User.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Manager deleted successfully";
            return RedirectToAction("ManagersList");
        }

        [HttpGet]
        public IActionResult DepartmentsList()
        {
            if (HttpContext.Session.GetString("Role") == "Admin")
            {
                var departmentlist = _db.Department.Include(a => a.User).ToList();
                return View(departmentlist);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            var ManagerNames = _db.User.Where(a => a.Role == "Manager").Select(u => u.Username).ToList();
            ViewBag.ManagerNames = ManagerNames;

            var ManagersfromUser = _db.User.Where(a => a.Role == "Manager" && a.Department == null).ToList();
            ViewBag.AllManagers = ManagersfromUser;
            return View();
        }

        [HttpPost]
        public IActionResult AddDepartment(DepartmentDTO departmentDTO)
        {
            if (ModelState.IsValid)
            {
                _db.Department.Add(_departmentMapper.Map(departmentDTO));
                _db.SaveChanges();
                TempData["success"] = "Department added successfully";
                return RedirectToAction("DepartmentsList");
            }

            return View(departmentDTO);
        }

        [HttpGet]
        public IActionResult EditDepartment(int? id)
            {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var departmentfromdb = _db.Department.Find(id);

            if (departmentfromdb == null)
            {
                return NotFound();
            }
            var ManagersfromUser = _db.User.Where(a => a.Role == "Manager" && a.Department == null).ToList();
            ViewBag.AllManagers = ManagersfromUser;
            var AssignedManagerName = _db.User.Where(d => d.UserId == departmentfromdb.UserId)
                                              .Select(d => d.Username)
                                              .FirstOrDefault();
            ViewBag.AssignedManagerName = AssignedManagerName;
            return View(departmentfromdb);
        }

        [HttpPost]
        public IActionResult EditDepartment(DepartmentDTO departmentDTO)
        {
            _db.Department.Update(_departmentMapper.Map(departmentDTO));
            _db.SaveChanges();
            TempData["success"] = "Department updated successfully";
            return RedirectToAction("DepartmentsList");
        }

        [HttpGet]
        public IActionResult DeleteDepartment(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var departmentfromdb = _db.Department.Find(id);

            if (departmentfromdb == null)
            {
                return NotFound();
            }
            var AssignedManagerName = _db.User.Where(d => d.UserId == departmentfromdb.UserId)
                                              .Select(d => d.Username)
                                              .FirstOrDefault();
            ViewBag.AssignedManagerName = AssignedManagerName;

            return View(departmentfromdb);
        }

        [HttpPost, ActionName("DeleteDepartment")]
        public IActionResult DeleteDepartmentPOST(int? id)
        {
            var obj = _db.Department.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Department.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Department deleted successfully";
            return RedirectToAction("DepartmentsList");
        }

        [HttpGet]
        public IActionResult EmployeesList()
        {
            if (HttpContext.Session.GetString("Role") == "Manager")
            {
                var loggedInManagerId = HttpContext.Session.GetInt32("UserId");
                var ManagerDepartmentName = _db.User.Where(m => m.UserId == loggedInManagerId)
                                                        .Select(d => d.Department.DepartmentName)
                                                        .FirstOrDefault();

                ViewBag.ManagerDepartmentName = ManagerDepartmentName;

                var Employees = _db.User.Where(a => a.Role == "Employee").ToList();
                return View(Employees);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            var loggedInManagerId = HttpContext.Session.GetInt32("UserId");
            var ManagerDepartmentName = _db.User.Where(m => m.UserId == loggedInManagerId)
                                                    .Select(d => d.Department.DepartmentName)
                                                    .FirstOrDefault();
            
            ViewBag.ManagerDepartmentName = ManagerDepartmentName;
            return View();            
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                _db.User.Add(_employeeMapper.Map(employeeDTO));
                _db.SaveChanges();
                TempData["success"] = "A new Employee was added successfully";
                return RedirectToAction("EmployeesList");
            }
            return View(employeeDTO);
        }

        [HttpGet]
        public IActionResult EditEmployee(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var Employeefromdb = _db.User.Find(id);

            if (Employeefromdb== null)
            {
                return NotFound();
            }

            return View(Employeefromdb);
        }

        [HttpPost]
        public IActionResult EditEmployee(EmployeeDTO employeeDTO)
        {
            _db.User.Update(_employeeMapper.Map(employeeDTO));
            _db.SaveChanges();
            TempData["success"] = "Employee updated successfully";
            return RedirectToAction("EmployeesList");
        }

    }
}
