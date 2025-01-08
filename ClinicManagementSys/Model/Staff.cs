using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Staff
{
    public int StaffId { get; set; }

    public string? StaffName { get; set; }

    public DateTime? Dob { get; set; }

    public DateTime? Doj { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Gender { get; set; }

    public int? DepartmentId { get; set; }

    public bool? StaffIsActive { get; set; }

    public decimal? Salary { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<LoginRegistration> LoginRegistrations { get; set; } = new List<LoginRegistration>();
}
