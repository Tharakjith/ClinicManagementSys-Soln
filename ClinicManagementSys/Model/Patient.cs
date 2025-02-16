using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Patient
{
    public int PatientId { get; set; }

    public string PatientName { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string? Gender { get; set; }

    public string? BloodGroup { get; set; }

    public string? PatientPhone { get; set; }

    public string? PatientAddress { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<MedicineBill> MedicineBills { get; set; } = new List<MedicineBill>();
}