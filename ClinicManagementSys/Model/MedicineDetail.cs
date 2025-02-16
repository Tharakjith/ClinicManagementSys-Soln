using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class MedicineDetail
{
    public int MedicineId { get; set; }

    public string MedicineName { get; set; } = null!;

    public DateTime ManufacturingDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public int CategoryId { get; set; }

    public decimal Cost { get; set; }

    public bool IsActive { get; set; }

    public virtual Category? Category { get; set; } = null!;

    public virtual ICollection<MedicineDistribution> MedicineDistributions { get; set; } = new List<MedicineDistribution>();

    public virtual ICollection<MedicineInventory> MedicineInventories { get; set; } = new List<MedicineInventory>();

    public virtual ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
