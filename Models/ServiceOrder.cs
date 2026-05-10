using System;
using System.Collections.Generic;

namespace EduTrip.Models;

public partial class ServiceOrder
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid ServiceId { get; set; }

    public Guid StudentId { get; set; }

    public Guid? AssignedMentorId { get; set; }

    public string Status { get; set; } = null!;

    public string? DeliverableUrl { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual MentorProfile? AssignedMentor { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
