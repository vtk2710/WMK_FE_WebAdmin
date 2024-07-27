using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class RecipeIngredientOrderDetail
{
    public Guid Id { get; set; }

    public Guid OrderDetailId { get; set; }

    public Guid RecipeId { get; set; }

    public Guid IngredientId { get; set; }

    public double Amount { get; set; }

    public double IngredientPrice { get; set; }

    public virtual OrderDetail OrderDetail { get; set; } = null!;
}
