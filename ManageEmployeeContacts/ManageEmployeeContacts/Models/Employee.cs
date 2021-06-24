using System;
using System.Collections.Generic;

#nullable disable

namespace ManageEmployeeContacts.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeFullname { get; set; }
        public DateTime EmployeeDob { get; set; }
        public string EmployeeGender { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeState { get; set; }
        public string EmployeeChangeDepartment { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
