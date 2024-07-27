using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class OrderDetail
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid RecipeId { get; set; }

    public int DayInWeek { get; set; }

    public int MealInDay { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<RecipeIngredientOrderDetail> RecipeIngredientOrderDetails { get; set; } = new List<RecipeIngredientOrderDetail>();
}
