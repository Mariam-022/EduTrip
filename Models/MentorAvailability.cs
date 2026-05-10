using System;
using System.Collections.Generic;

namespace EduTrip.Models;

public partial class MentorAvailability
{
    public Guid Id { get; set; }

    public Guid MentorId { get; set; }

    public string DayOfWeek { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool IsRecurring { get; set; }

    public bool IsBooked { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual MentorProfile Mentor { get; set; } = null!;
}
