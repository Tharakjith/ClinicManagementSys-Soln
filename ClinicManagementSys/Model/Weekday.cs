using System;
using System.Collections.Generic;

namespace ClinicManagementSys.Model;

public partial class Weekday
{
    public int WeekdaysId { get; set; }

    public string? WeekdaysName { get; set; }

    public virtual ICollection<Timeslot> Timeslots { get; set; } = new List<Timeslot>();
}
