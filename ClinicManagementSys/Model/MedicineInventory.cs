using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class MedicineInventory
{
    public int MedicineStockId { get; set; }

    public int MedicineId { get; set; }

    public int StockInHand { get; set; }

    public int ReOrderLevel { get; set; }

    public int Purchase { get; set; }

    public int Issuance { get; set; }

    public bool IsActive { get; set; }

    public virtual MedicineDetail Medicine { get; set; } = null!;
}
