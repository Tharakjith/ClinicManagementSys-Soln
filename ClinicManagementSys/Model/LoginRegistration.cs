using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class LoginRegistration
{
    public int RegistrationId { get; set; }

    public int StaffId { get; set; }

    public int RoleId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public bool RisActive { get; set; }

    public DateTime RegisteredDate { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual Role? Role { get; set; }

    public virtual Staff? Staff { get; set; }
    public string StaffName { get; set; }  // For staff name
    public string RoleName { get; set; }
}
