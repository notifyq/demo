using System;
using System.Collections.Generic;

namespace demo_1.Model
{
    public partial class Status
    {
        public Status()
        {
            RequestStatuses = new HashSet<RequestStatus>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; } = null!;

        public virtual ICollection<RequestStatus> RequestStatuses { get; set; }
    }
}
