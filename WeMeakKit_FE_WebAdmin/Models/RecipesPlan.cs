using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class RecipesPlan
{
    public Guid Id { get; set; }

    public Guid RecipeId { get; set; }

    public Guid StandardWeeklyPlanId { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public int DayInWeek { get; set; }

    public int MealInDay { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;

    public virtual WeeklyPlan StandardWeeklyPlan { get; set; } = null!;
}
