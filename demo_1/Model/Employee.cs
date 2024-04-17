using System;
using System.Collections.Generic;

namespace demo_1.Model
{
    public partial class Employee
    {
        public Employee()
        {
            RequestDesignatedEmployees = new HashSet<RequestDesignatedEmployee>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string EmployeeSurname { get; set; } = null!;
        public string? EmployeePatronymic { get; set; }
        public string EmployeeLogin { get; set; } = null!;
        public string EmployeePassword { get; set; } = null!;
        public int EmployeeRole { get; set; }

        public virtual Role EmployeeRoleNavigation { get; set; } = null!;
        public virtual ICollection<RequestDesignatedEmployee> RequestDesignatedEmployees { get; set; }
    }
}
