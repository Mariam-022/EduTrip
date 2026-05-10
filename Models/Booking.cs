using System;
using System.Collections.Generic;

namespace EduTrip.Models;

public partial class Booking
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid StudentId { get; set; }

    public Guid MentorId { get; set; }

    public Guid AvailabilityId { get; set; }

    public DateTime ScheduledAt { get; set; }

    public int DurationMinutes { get; set; }

    public string? SessionLink { get; set; }

    public string Status { get; set; } = null!;

    public int? StudentRating { get; set; }

    public string? StudentReview { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual MentorAvailability Availability { get; set; } = null!;

    public virtual MentorProfile Mentor { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
