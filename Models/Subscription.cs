using System;
using System.Collections.Generic;

namespace EduTrip.Models;

public partial class Subscription
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Plan { get; set; } = null!;

    public string BillingCycle { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime? NextBillingDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
