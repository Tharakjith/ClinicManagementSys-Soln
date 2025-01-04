using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }

    public int SpecializationId { get; set; }

    public int DoctorId { get; set; }

 

    public DateTime AppointmentDate { get; set; }

    public int DailyAvailabilityId { get; set; }

    public int TokenNumber { get; set; }

    public int AppointmentStatusId { get; set; }

    public virtual AppointmentStatus AppointmentStatus { get; set; } = null!;

    public virtual DailyAvailability DailyAvailability { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ICollection<LabTestReport> LabTestReports { get; set; } = new List<LabTestReport>();

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<PatientBill> PatientBills { get; set; } = new List<PatientBill>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual Specialization Specialization { get; set; } = null!;

    public virtual ICollection<StartDiagnosy> StartDiagnosies { get; set; } = new List<StartDiagnosy>();

    public virtual ICollection<TestPrescription> TestPrescriptions { get; set; } = new List<TestPrescription>();

}
