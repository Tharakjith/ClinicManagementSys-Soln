using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Labtest
{
    public int LabTestId { get; set; }

    public string? TestName { get; set; }

    public decimal? Price { get; set; }

    public decimal? LowRange { get; set; }

    public decimal? HighRange { get; set; }

    public string? Sample { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<LabTestReport> LabTestReports { get; set; } = new List<LabTestReport>();

    public virtual ICollection<TestPrescription> TestPrescriptions { get; set; } = new List<TestPrescription>();
}