using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class RecipeStep
{
    public Guid Id { get; set; }

    public Guid RecipeId { get; set; }

    public int Index { get; set; }

    public string? MediaUrl { get; set; }

    public string? ImageLink { get; set; }

    public string? Description { get; set; }

    public string Name { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
