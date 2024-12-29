using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class LabTestBill
{
    public int LabTestBillId { get; set; }

    public int LtreportId { get; set; }

    public decimal TestPrice { get; set; }

    public DateTime? TestDate { get; set; }

    public int? LabTestBillStatusId { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual LabTestBillStatus? LabTestBillStatus { get; set; }

    public virtual LabTestReport Ltreport { get; set; } = null!;
}
