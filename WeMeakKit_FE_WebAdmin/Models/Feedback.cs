using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class Feedback
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public int Rating { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual Order Order { get; set; } = null!;
}
