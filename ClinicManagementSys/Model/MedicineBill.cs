using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class MedicineBill
{
    public int MedicineBillId { get; set; }

    public int? MedDistId { get; set; }

    public int? DoctorId { get; set; }

    public int? PatientId { get; set; }

    public decimal TotalAmount { get; set; }

    public int? PaymentStatusId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Remarks { get; set; }

    public virtual Doctor? Doctor { get; set; }

    public virtual MedicineDistribution? MedDist { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual MedicineBillStatus? PaymentStatus { get; set; }
}
