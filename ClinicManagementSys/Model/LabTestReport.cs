using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class LabTestReport
{
    public int LtreportId { get; set; }

    public int AppointmentId { get; set; }

    public int LabTestId { get; set; }

    public int? HighRange { get; set; }

    public int? LowRange { get; set; }

    public int? ActualResult { get; set; }

    public string? Remarks { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual Labtest LabTest { get; set; } = null!;

    public virtual ICollection<LabTestBill> LabTestBills { get; set; } = new List<LabTestBill>();
}
