using System;
using System.Collections.Generic;

namespace demo_1.Model
{
    public partial class RequestDesignatedEmployee
    {
        public int RequestDesignatedEmployeeId { get; set; }
        public int EmployeeId { get; set; }
        public int RequestId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Request Request { get; set; } = null!;
    }
}
