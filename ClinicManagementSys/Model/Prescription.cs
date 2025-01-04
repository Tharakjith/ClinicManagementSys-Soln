using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public int? AppointmentId { get; set; }

    public int MedicineId { get; set; }

    public string Dosage { get; set; } = null!;

    public string Frequency { get; set; } = null!;

    public int? NumberofDays { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual MedicineDetail? Medicine { get; set; }

    public virtual ICollection<MedicineDistribution> MedicineDistributions { get; set; } = new List<MedicineDistribution>();
}
