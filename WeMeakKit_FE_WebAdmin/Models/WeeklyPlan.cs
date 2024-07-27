using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class WeeklyPlan
{
    public Guid Id { get; set; }

    public DateTime? BeginDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Description { get; set; }

    public DateTime CreateAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ApprovedAt { get; set; }

    public string? ApprovedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public int ProcessStatus { get; set; }

    public string? Notice { get; set; }

    public string? Title { get; set; }

    public string? UrlImage { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<RecipesPlan> RecipesPlans { get; set; } = new List<RecipesPlan>();
}
