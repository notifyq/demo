using System;
using System.Collections.Generic;

namespace demo_1.Model
{
    public partial class Request
    {
        public Request()
        {
            RequestComments = new HashSet<RequestComment>();
            RequestDesignatedEmployees = new HashSet<RequestDesignatedEmployee>();
            RequestEquipmentLists = new HashSet<RequestEquipmentList>();
            RequestStatuses = new HashSet<RequestStatus>();
        }

        public int RequestId { get; set; }
        public DateOnly? RequestDate { get; set; }
        public string ClientName { get; set; } = null!;
        public string? ClientSurname { get; set; }
        public string? ClientPatronymic { get; set; }
        public string RequestDescription { get; set; } = null!;

        public virtual ICollection<RequestComment> RequestComments { get; set; }
        public virtual ICollection<RequestDesignatedEmployee> RequestDesignatedEmployees { get; set; }
        public virtual ICollection<RequestEquipmentList> RequestEquipmentLists { get; set; }
        public virtual ICollection<RequestStatus> RequestStatuses { get; set; }
    }
}
