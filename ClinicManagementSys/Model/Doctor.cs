using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public int? SpecializationId { get; set; }

    public decimal? ConsultationFee { get; set; }

    public int? RegistrationId { get; set; }

    public bool? DoctorIsActive { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Availability> Availabilities { get; set; } = new List<Availability>();

    public virtual ICollection<MedicineBill> MedicineBills { get; set; } = new List<MedicineBill>();

    public virtual LoginRegistration? Registration { get; set; }

    public virtual Specialization? Specialization { get; set; }

    public virtual ICollection<StartDiagnosy> StartDiagnosies { get; set; } = new List<StartDiagnosy>();
}
