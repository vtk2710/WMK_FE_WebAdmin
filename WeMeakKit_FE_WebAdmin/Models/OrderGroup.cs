using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class OrderGroup
{
    public Guid Id { get; set; }

    public Guid ShipperId { get; set; }

    public string Location { get; set; } = null!;

    public DateTime AsignAt { get; set; }

    public Guid AsignBy { get; set; }

    public int Status { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User Shipper { get; set; } = null!;
}
