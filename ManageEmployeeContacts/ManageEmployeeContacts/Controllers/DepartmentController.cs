using ManageEmployeeContacts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEmployeeContacts.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ManageEmployeeContactsContext _context = new ManageEmployeeContactsContext();

        // GET: DepartmentController
        public ActionResult Index(string _name)
        {
            var list = _context.Departments.ToList();

            if (!String.IsNullOrEmpty(_name))
            {
                list = _context.Departments.Where(s => s.DepartmentName.Contains(_name)).ToList();

            }

            if (TempData["messageStyle"] != null && TempData["messageStatus"] != null && TempData["messageContent"] != null)
            {
                ViewBag.messageStyle = TempData["messageStyle"].ToString();
                ViewBag.messageStatus = TempData["messageStatus"].ToString();
                ViewBag.messageContent = TempData["messageContent"].ToString();
            }
            return View(list);
        }

        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            var department = _context.Departments.FirstOrDefault(_d => _d.DepartmentId == id);
            var employees = _context.Employees.Where(_e => _e.DepartmentId == id).ToList();

            dynamic mymodel = new ExpandoObject();
            mymodel.Department = department;
            mymodel.Employees = employees;

            return View(mymodel);
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            try
            {
                if(department.DepartmentName.Length == 0)
                {
                    return View();
                }
                _context.Departments.Add(department);
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

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id)
        {
            var department = _context.Departments.FirstOrDefault(_d => _d.DepartmentId == id);
            return View(department);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Department department)
        {
            try
            {
                var _department = _context.Departments.FirstOrDefault(_d => _d.DepartmentId == id);
                _department.DepartmentName = department.DepartmentName;
                _context.SaveChanges();

                TempData["messageStyle"] = "alert-success";
                TempData["messageStatus"] = "Thành công";
                TempData["messageContent"] = "Thay đổi thành công";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Delete/5
        public ActionResult Delete(int id)
        {
            var department = _context.Departments.FirstOrDefault(_d => _d.DepartmentId == id);
            return View(department);
        }

        // POST: DepartmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Department department)
        {
            try
            {
                department = _context.Departments.FirstOrDefault(_d => _d.DepartmentId == id);

                var employees = _context.Employees.Where(_e => _e.DepartmentId == id).ToList();
                employees.ForEach(_e => _context.Employees.Remove(_e));

                _context.Departments.Remove(department);
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
    }
}
