using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<MedicineDetail> MedicineDetails { get; set; } = new List<MedicineDetail>();
}
