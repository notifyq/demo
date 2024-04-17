using System;
using System.Collections.Generic;

namespace demo_1.Model
{
    public partial class RequestStatus
    {
        public int RequestStatusId { get; set; }
        public int RequestId { get; set; }
        public int StatusId { get; set; }
        public DateTime ChangeDate { get; set; }

        public virtual Request Request { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
    }
}
