using ManageEmployeeContacts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEmployeeContacts.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ManageEmployeeContactsContext _context = new ManageEmployeeContactsContext();
        // GET: EmployeeController
        public ActionResult Index(string _name)
        {
            var list = _context.Employees.ToList();
            list
                .ForEach(_e => _e.Department = _context.Departments
                .FirstOrDefault(_d => _d.DepartmentId == _e.DepartmentId));

            if (!String.IsNullOrEmpty(_name))
            {
                list = _context.Employees.Where(_e=> _e.EmployeeFullname.Contains(_name)).ToList();
                list
                    .ForEach(_e => _e.Department = _context.Departments
                    .FirstOrDefault(_d => _d.DepartmentId == _e.DepartmentId));

            }

            if (TempData["messageStyle"] != null && TempData["messageStatus"] != null && TempData["messageContent"] != null)
            {
                ViewBag.messageStyle = TempData["messageStyle"].ToString();
                ViewBag.messageStatus = TempData["messageStatus"].ToString();
                ViewBag.messageContent = TempData["messageContent"].ToString();
            }
            return View(list);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            var employee = _context.Employees.FirstOrDefault(_e => _e.EmployeeId == id);

            employee.Department = _context.Departments.FirstOrDefault(_d => _d.DepartmentId == employee.DepartmentId);
            ViewBag.Departments = _context.Departments.ToList();

            if (TempData["messageStyle"] != null && TempData["messageStatus"] != null && TempData["messageContent"] != null)
            {
                ViewBag.messageStyle = TempData["messageStyle"].ToString();
                ViewBag.messageStatus = TempData["messageStatus"].ToString();
                ViewBag.messageContent = TempData["messageContent"].ToString();
            }
            return View(employee);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList();
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {
                employee.EmployeeChangeDepartment = "Không";
                employee.EmployeeState = "Đang làm việc";
                _context.Employees.Add(employee);
                _context.SaveChanges();

                TempData["messageStyle"] = "alert-success";
                TempData["messageStatus"] = "Thành công";
                TempData["messageContent"] = "Tạo mới thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = _context.Employees.FirstOrDefault(_e => _e.EmployeeId == id);

            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee employee)
        {
            try
            {
                var _employee = _context.Employees.FirstOrDefault(_e => _e.EmployeeId == id);
                _employee.EmployeeFullname = employee.EmployeeFullname;
                _employee.EmployeeDob = employee.EmployeeDob;
                _employee.EmployeeGender = employee.EmployeeGender;
                _employee.EmployeeEmail = employee.EmployeeEmail;
                _employee.EmployeePhone = employee.EmployeePhone;
                _context.SaveChanges();

                TempData["messageStyle"] = "alert-success";
                TempData["messageStatus"] = "Thành công";
                TempData["messageContent"] = "Sửa đổi thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            var employee = _context.Employees.FirstOrDefault(_e => _e.EmployeeId == id);
            employee.Department = _context.Departments.FirstOrDefault(_d => _d.DepartmentId == employee.DepartmentId);

            return View(employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                var _employee = _context.Employees.FirstOrDefault(_e => _e.EmployeeId == id);
                _context.Employees.Remove(_employee);
                _context.SaveChanges();

                TempData["messageStyle"] = "alert-success";
                TempData["messageStatus"] = "Thành công";
                TempData["messageContent"] = "Xóa thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeState(int EmployeeId, string EmployeeState)
        {
            try
            {
                var _employee = _context.Employees.FirstOrDefault(_e => _e.EmployeeId == EmployeeId);
                _employee.EmployeeState = EmployeeState;
                _context.SaveChanges();

                TempData["messageStyle"] = "alert-success";
                TempData["messageStatus"] = "Thành công";
                TempData["messageContent"] = "Thay đổi thành công";
                return RedirectToAction("Details", new { id = EmployeeId });
            }
            catch
            {
                return RedirectToAction("Details", new { id = EmployeeId });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeDepartment(int EmployeeId, int DepartmentId)
        {
            try
            {
                var _employee = _context.Employees.FirstOrDefault(_e => _e.EmployeeId == EmployeeId);

                var _department = _context.Departments.FirstOrDefault(_d => _d.DepartmentId == _employee.DepartmentId);

                _employee.EmployeeChangeDepartment = "Chuyển từ phòng " + _department.DepartmentName;
                _employee.DepartmentId = DepartmentId;


                _context.SaveChanges();

                TempData["messageStyle"] = "alert-success";
                TempData["messageStatus"] = "Thành công";
                TempData["messageContent"] = "Thay đổi thành công";
                return RedirectToAction("Details", new { id = EmployeeId });
            }
            catch
            {
                return RedirectToAction("Details", new { id = EmployeeId });
            }
        }

        public ActionResult ExportExcel(int id)
        {
            var employees = _context.Employees.ToList();
            employees
                .ForEach(_e => _e.Department = _context.Departments
                .FirstOrDefault(_d => _d.DepartmentId == _e.DepartmentId));
            if (id != 0)
            {
                employees = _context.Employees.Where(_e => _e.DepartmentId == id).ToList();
                employees
                .ForEach(_e => _e.Department = _context.Departments
                .FirstOrDefault(_d => _d.DepartmentId == _e.DepartmentId));
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var Ep = new ExcelPackage();
            var Sheet = Ep.Workbook.Worksheets.Add("Report");

            Sheet.Cells["A1"].Value = "Export Employees";
            Sheet.Cells["A2"].Value = "Date Create";
            Sheet.Cells["B2"].Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            Sheet.Cells["A3"].Value = "Phòng";
            Sheet.Cells["B3"].Value = id != 0 ? _context.Departments.FirstOrDefault(_d => _d.DepartmentId == id).DepartmentName : "Tất cả";

            Sheet.Cells["A5"].Value = "Họ và tên";
            Sheet.Cells["B5"].Value = "Ngày sinh";
            Sheet.Cells["C5"].Value = "Giới tính";
            Sheet.Cells["D5"].Value = "Email";
            Sheet.Cells["E5"].Value = "Số điện thoại";
            Sheet.Cells["F5"].Value = "Trạng thái";
            Sheet.Cells["G5"].Value = "Chuyển công tác";
            Sheet.Cells["H5"].Value = "Phòng làm việc";

            int rowStart = 6;

            foreach (var _e in employees)
            {
                Sheet.Cells[string.Format("A{0}", rowStart)].Value = _e.EmployeeFullname;
                Sheet.Cells[string.Format("B{0}", rowStart)].Value = _e.EmployeeDob.ToString("MM/dd/yyyy");
                Sheet.Cells[string.Format("C{0}", rowStart)].Value = _e.EmployeeGender;
                Sheet.Cells[string.Format("D{0}", rowStart)].Value = _e.EmployeeEmail;
                Sheet.Cells[string.Format("E{0}", rowStart)].Value = _e.EmployeePhone;
                Sheet.Cells[string.Format("F{0}", rowStart)].Value = _e.EmployeeState;
                Sheet.Cells[string.Format("G{0}", rowStart)].Value = _e.EmployeeChangeDepartment;
                Sheet.Cells[string.Format("H{0}", rowStart)].Value = _e.Department.DepartmentName;

                rowStart++;
            }
            Sheet.Cells["A:AZ"].AutoFitColumns();
            return File(Ep.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reports.xlsx");
        }


    }
}
