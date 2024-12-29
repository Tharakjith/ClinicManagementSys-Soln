using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class BillStatus
{
    public int BillStatusId { get; set; }

    public string? BillStatus1 { get; set; }

    public virtual ICollection<PatientBill> PatientBills { get; set; } = new List<PatientBill>();
}
