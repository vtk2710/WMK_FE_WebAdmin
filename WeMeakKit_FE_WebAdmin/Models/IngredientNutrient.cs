﻿using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class IngredientNutrient
{
    public Guid Id { get; set; }

    public Guid IngredientId { get; set; }

    public double Calories { get; set; }

    public double Fat { get; set; }

    public double SaturatedFat { get; set; }

    public double Sugar { get; set; }

    public double Carbonhydrate { get; set; }

    public double DietaryFiber { get; set; }

    public double Protein { get; set; }

    public double Sodium { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;
}
