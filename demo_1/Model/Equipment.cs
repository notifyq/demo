using System;
using System.Collections.Generic;

namespace demo_1.Model
{
    public partial class Equipment
    {
        public Equipment()
        {
            RequestEquipmentLists = new HashSet<RequestEquipmentList>();
        }

        public int EquipmentId { get; set; }
        public string EquipmentSerialNumber { get; set; } = null!;
        public string? EquipmentName { get; set; }
        public int DefectTypeId { get; set; }

        public virtual DefectType DefectType { get; set; } = null!;
        public virtual ICollection<RequestEquipmentList> RequestEquipmentLists { get; set; }
    }
}
