using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class MedDistributionStatus
{
    public int MedStatusId { get; set; }

    public string? MedStatusName { get; set; }

    public virtual ICollection<MedicineDistribution> MedicineDistributions { get; set; } = new List<MedicineDistribution>();
}
