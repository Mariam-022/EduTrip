using System;
using System.Collections.Generic;

namespace EduTrip.Models;

public partial class Package
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int DurationDays { get; set; }

    public int MentorshipCredits { get; set; }

    public bool HasContentAccess { get; set; }

    public bool HasPriorityBooking { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<PackageEnrollment> PackageEnrollments { get; set; } = new List<PackageEnrollment>();
}
