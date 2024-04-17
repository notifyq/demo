using System;
using System.Collections.Generic;

namespace demo_1.Model
{
    public partial class RequestEquipmentList
    {
        public int RequestEquipmentListId { get; set; }
        public int EquipmentId { get; set; }
        public int RequestId { get; set; }

        public virtual Equipment Equipment { get; set; } = null!;
        public virtual Request Request { get; set; } = null!;
    }
}
