using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class MedicineDetail
{
    public int MedicineId { get; set; }

    public string MedicineName { get; set; } = null!;

    public DateTime ManufacturingDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public int MedicineCategoryId { get; set; }

    public string Unit { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<MedicineDistribution> MedicineDistributions { get; set; } = new List<MedicineDistribution>();

    public virtual ICollection<MedicineInventory> MedicineInventories { get; set; } = new List<MedicineInventory>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
