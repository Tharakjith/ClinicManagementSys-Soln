using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class LabTestBillStatus
{
    public int LabTestBillStatusId { get; set; }

    public string? LabTestBillStatus1 { get; set; }

    public virtual ICollection<LabTestBill> LabTestBills { get; set; } = new List<LabTestBill>();
}
