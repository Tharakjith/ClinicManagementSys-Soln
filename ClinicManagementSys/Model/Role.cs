using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<LoginRegistration> LoginRegistrations { get; set; } = new List<LoginRegistration>();
}
