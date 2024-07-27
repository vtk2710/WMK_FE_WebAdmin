using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class Category
{
    public Guid Id { get; set; }

    public string Type { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Status { get; set; }

    public virtual ICollection<RecipeCategory> RecipeCategories { get; set; } = new List<RecipeCategory>();
}
