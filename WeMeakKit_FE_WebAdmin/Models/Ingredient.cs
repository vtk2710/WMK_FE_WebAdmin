using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class Ingredient
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Img { get; set; }

    public string Unit { get; set; } = null!;

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public Guid IngredientCategoryId { get; set; }

    public double Price { get; set; }

    public string PackagingMethod { get; set; } = null!;

    public string PreservationMethod { get; set; } = null!;

    public virtual IngredientCategory IngredientCategory { get; set; } = null!;

    public virtual IngredientNutrient? IngredientNutrient { get; set; }

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
}
