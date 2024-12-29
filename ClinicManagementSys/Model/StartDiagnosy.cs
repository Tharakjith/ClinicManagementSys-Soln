using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class StartDiagnosy
{
    public int HistoryId { get; set; }

    public string? Diagnosis { get; set; }

    public string? Symptoms { get; set; }

    public DateTime? NextVisiting { get; set; }

    public string? DoctorNote { get; set; }

    public DateTime? DiagnosysDate { get; set; }

    public int? Reference { get; set; }

    public int? AppointmentId { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Doctor? ReferenceNavigation { get; set; }
}
