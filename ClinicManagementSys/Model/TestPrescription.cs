using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class TestPrescription
{
    public int TpId { get; set; }

    public int? AppointmentId { get; set; }

    public int? LabTestId { get; set; }

    public string? SampleItem { get; set; }

    public virtual Appointment? Appointments { get; set; }

    public virtual Labtest? LabTest { get; set; }
}
