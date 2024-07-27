using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class RecipeCategory
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public Guid RecipeId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
