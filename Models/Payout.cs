using System;
using System.Collections.Generic;

namespace EduTrip.Models;

public partial class Payout
{
    public Guid Id { get; set; }

    public Guid MentorId { get; set; }

    public decimal TotalAmount { get; set; }

    public string Currency { get; set; } = null!;

    public string Method { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? ProcessedAt { get; set; }

    public Guid? InitiatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? InitiatedByNavigation { get; set; }

    public virtual MentorProfile Mentor { get; set; } = null!;

    public virtual ICollection<MentorEarning> MentorEarnings { get; set; } = new List<MentorEarning>();
}
