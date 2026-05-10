using System;
using System.Collections.Generic;

namespace EduTrip.Models;

public partial class MentorProfile
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? Bio { get; set; }

    public string Specialties { get; set; } = null!;

    public decimal HourlyRate { get; set; }

    public int YearsExperience { get; set; }

    public decimal? AverageRating { get; set; }

    public bool IsApproved { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<MentorAvailability> MentorAvailabilities { get; set; } = new List<MentorAvailability>();

    public virtual ICollection<MentorEarning> MentorEarnings { get; set; } = new List<MentorEarning>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payout> Payouts { get; set; } = new List<Payout>();

    public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();

    public virtual User User { get; set; } = null!;
}
