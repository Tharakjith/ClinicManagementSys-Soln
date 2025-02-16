using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class MedicineDistribution
{
    public int MedDistId { get; set; }

    public int? PrescriptionId { get; set; }

    public int? MedicineId { get; set; }

    public int QuantityDistributed { get; set; }

    public DateTime? DistributionDate { get; set; }

    public int? MedStatusId { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual MedDistributionStatus? MedStatus { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual MedicineDetail? Medicine { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<MedicineBill> MedicineBills { get; set; } = new List<MedicineBill>();
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Prescription? Prescription { get; set; }
}
