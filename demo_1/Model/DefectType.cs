using System;
using System.Collections.Generic;

namespace demo_1.Model
{
    public partial class DefectType
    {
        public DefectType()
        {
            Equipment = new HashSet<Equipment>();
        }

        public int DefectTypeId { get; set; }
        public string DefectName { get; set; } = null!;

        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
