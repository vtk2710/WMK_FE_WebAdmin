using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class Recipe
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int ServingSize { get; set; }

    public int Difficulty { get; set; }

    public string? Description { get; set; }

    public string? Img { get; set; }

    public double Price { get; set; }

    public int Popularity { get; set; }

    public int ProcessStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ApprovedAt { get; set; }

    public string? ApprovedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public string? Notice { get; set; }

    public int BaseStatus { get; set; }

    public int CookingTime { get; set; }

    public virtual ICollection<RecipeCategory> RecipeCategories { get; set; } = new List<RecipeCategory>();

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    public virtual RecipeNutrient? RecipeNutrient { get; set; }

    public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = new List<RecipeStep>();

    public virtual ICollection<RecipesPlan> RecipesPlans { get; set; } = new List<RecipesPlan>();
}
