using System;
using System.Collections.Generic;

namespace EduTrip.Models;

public partial class PackageEnrollment
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid StudentId { get; set; }

    public Guid PackageId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int RemainingCredits { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Package Package { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
