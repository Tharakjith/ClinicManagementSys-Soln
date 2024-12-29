using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class MedicineBillStatus
{
    public int PaymentStatusId { get; set; }

    public string? PaymentStatusName { get; set; }

    public virtual ICollection<MedicineBill> MedicineBills { get; set; } = new List<MedicineBill>();
}
