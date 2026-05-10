using System;
using System.Collections.Generic;

namespace EduTrip.Models;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string OrderType { get; set; } = null!;

    public Guid? ServiceId { get; set; }

    public Guid? PackageId { get; set; }

    public Guid? MentorId { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual MentorProfile? Mentor { get; set; }

    public virtual MentorEarning? MentorEarning { get; set; }

    public virtual Package? Package { get; set; }

    public virtual PackageEnrollment? PackageEnrollment { get; set; }

    public virtual Payment? Payment { get; set; }

    public virtual Service? Service { get; set; }

    public virtual ServiceOrder? ServiceOrder { get; set; }

    public virtual User User { get; set; } = null!;
}
