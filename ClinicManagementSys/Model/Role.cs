using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<LoginRegistration> LoginRegistrations { get; set; } = new List<LoginRegistration>();
}
