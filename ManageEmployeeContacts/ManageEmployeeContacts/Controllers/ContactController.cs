using ManageEmployeeContacts.Models;
using ManageEmployeeContacts.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEmployeeContacts.Controllers
{
    public class ContactController : Controller
    {
        private readonly ManageEmployeeContactsContext _context = new ManageEmployeeContactsContext();
        private readonly EmailServices _emailServices = new EmailServices();
        public IActionResult Index(int _DepartmentId, string _name)
        {
            try
            {
                var list = _context.Employees.ToList();
                list
                    .ForEach(_e => _e.Department = _context.Departments
                    .FirstOrDefault(_d => _d.DepartmentId == _e.DepartmentId));

                if (!String.IsNullOrEmpty(_name))
                {
                    list = _context.Employees.Where(_e => _e.EmployeeFullname.Contains(_name)).ToList();
                    list
                        .ForEach(_e => _e.Department = _context.Departments
                        .FirstOrDefault(_d => _d.DepartmentId == _e.DepartmentId));

                }
                if (_DepartmentId != 0)
                {
                    list = _context.Employees.Where(_e => _e.DepartmentId == _DepartmentId).ToList();
                    list
                        .ForEach(_e => _e.Department = _context.Departments
                        .FirstOrDefault(_d => _d.DepartmentId == _e.DepartmentId));

                }
                if (_DepartmentId != 0 && !String.IsNullOrEmpty(_name))
                {
                    list = _context.Employees.Where(_e => _e.DepartmentId == _DepartmentId && _e.EmployeeFullname.Contains(_name)).ToList();
                    list
                        .ForEach(_e => _e.Department = _context.Departments
                        .FirstOrDefault(_d => _d.DepartmentId == _e.DepartmentId));
                }
                ViewBag.Departments = _context.Departments.ToList();

                if (TempData["messageStyle"] !=null && TempData["messageStatus"] != null && TempData["messageContent"] != null)
                {
                    ViewBag.messageStyle = TempData["messageStyle"].ToString();
                    ViewBag.messageStatus = TempData["messageStatus"].ToString();
                    ViewBag.messageContent = TempData["messageContent"].ToString();
                }
                return View(list);
            }
            catch {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendEmail(string _listEmployeeId, string _subject, string _content)
        {
            try
            {
                if ( String.IsNullOrEmpty(_listEmployeeId) || String.IsNullOrEmpty(_subject) || String.IsNullOrEmpty(_content))
                {
                    TempData["messageStyle"] = "alert-danger";
                    TempData["messageStatus"] = "Thất bại";
                    TempData["messageContent"] = "Gửi Email thất bại. Hãy nhập đủ thông tin";
                    return RedirectToAction(nameof(Index));
                }

                var listEmployeeId = _listEmployeeId.Split(' ').Select(Int32.Parse).ToList();
                var employees = new List<Employee>();
                listEmployeeId.ForEach(_id => employees.Add(_context.Employees.FirstOrDefault(_e => _e.EmployeeId == _id)));

                employees.ForEach(_e => _emailServices.SendEmail(_e.EmployeeEmail, _subject, _content));

                TempData["messageStyle"] = "alert-success";
                TempData["messageStatus"] = "Thành công";
                TempData["messageContent"] = "Gửi Email thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {

                TempData["messageStyle"] = "alert-danger";
                TempData["messageStatus"] = "Thất bại";
                TempData["messageContent"] = "Gửi Email thất bại. Hãy nhập đủ thông tin";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
