using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class RecipeIngredient
{
    public Guid Id { get; set; }

    public Guid RecipeId { get; set; }

    public Guid IngredientId { get; set; }

    public double Amount { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
