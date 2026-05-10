using System;
using System.Collections.Generic;

namespace EduTrip.Models;

public partial class MentorEarning
{
    public Guid Id { get; set; }

    public Guid MentorId { get; set; }

    public Guid OrderId { get; set; }

    public decimal GrossAmount { get; set; }

    public decimal MentorShare { get; set; }

    public decimal PlatformShare { get; set; }

    public decimal CommissionRate { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? ClearedAt { get; set; }

    public DateTime? PaidOutAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? PayoutId { get; set; }

    public virtual MentorProfile Mentor { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Payout? Payout { get; set; }
}
