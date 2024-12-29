using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class PatientBill
{
    public int PatientBillId { get; set; }

    public int AppointmentId { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public int? DoctorId { get; set; }

    public decimal ConsultationFee { get; set; }

    public decimal RegistrationFee { get; set; }

    public decimal? TotalFee { get; set; }

    public int BillStatusId { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual BillStatus BillStatus { get; set; } = null!;
}
