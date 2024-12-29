using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Timeslot
{
    public int TimeSlotId { get; set; }

    public TimeSpan? StartTime { get; set; }

    public TimeSpan? EndTime { get; set; }

    public int? WeekdaysId { get; set; }

    public virtual ICollection<Availability> Availabilities { get; set; } = new List<Availability>();

    public virtual Weekday? Weekdays { get; set; }
}
