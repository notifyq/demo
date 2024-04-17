using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_1.Model
{
    public partial class Employee
    {

        public string EmployeeFIO
        { 
            get 
            {
                return $"{EmployeeSurname} {EmployeeName} {EmployeePatronymic}";
            }
        } 
    }
}
