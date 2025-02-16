using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
}
