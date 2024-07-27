using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid? OrderGroupId { get; set; }

    public Guid? StanderdWeeklyPlanId { get; set; }

    public string? Note { get; set; }

    public string Address { get; set; } = null!;

    public DateTime ShipDate { get; set; }

    public DateTime OrderDate { get; set; }

    public double TotalPrice { get; set; }

    public int Status { get; set; }

    public string? Img { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual OrderGroup? OrderGroup { get; set; }

    public virtual WeeklyPlan? StanderdWeeklyPlan { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;
}
