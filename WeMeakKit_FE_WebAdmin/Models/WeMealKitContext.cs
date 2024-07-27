using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class WeMealKitContext : DbContext
{
    public WeMealKitContext()
    {
    }

    public WeMealKitContext(DbContextOptions<WeMealKitContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientCategory> IngredientCategorys { get; set; }

    public virtual DbSet<IngredientNutrient> IngredientNutrients { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderGroup> OrderGroups { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeCategory> RecipeCategories { get; set; }

    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    public virtual DbSet<RecipeIngredientOrderDetail> RecipeIngredientOrderDetails { get; set; }

    public virtual DbSet<RecipeNutrient> RecipeNutrients { get; set; }

    public virtual DbSet<RecipeStep> RecipeSteps { get; set; }

    public virtual DbSet<RecipesPlan> RecipesPlans { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WeeklyPlan> WeeklyPlans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_Feedbacks_OrderId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks).HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasIndex(e => e.IngredientCategoryId, "IX_Ingredients_IngredientCategoryId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PackagingMethod).HasDefaultValue("");
            entity.Property(e => e.PreservationMethod).HasDefaultValue("");

            entity.HasOne(d => d.IngredientCategory).WithMany(p => p.Ingredients).HasForeignKey(d => d.IngredientCategoryId);
        });

        modelBuilder.Entity<IngredientCategory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<IngredientNutrient>(entity =>
        {
            entity.HasIndex(e => e.IngredientId, "IX_IngredientNutrients_IngredientID").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IngredientId).HasColumnName("IngredientID");

            entity.HasOne(d => d.Ingredient).WithOne(p => p.IngredientNutrient).HasForeignKey<IngredientNutrient>(d => d.IngredientId);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.OrderGroupId, "IX_Orders_OrderGroupId");

            entity.HasIndex(e => e.StanderdWeeklyPlanId, "IX_Orders_StanderdWeeklyPlanId");

            entity.HasIndex(e => e.UserId, "IX_Orders_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.OrderGroup).WithMany(p => p.Orders).HasForeignKey(d => d.OrderGroupId);

            entity.HasOne(d => d.StanderdWeeklyPlan).WithMany(p => p.Orders).HasForeignKey(d => d.StanderdWeeklyPlanId);

            entity.HasOne(d => d.User).WithMany(p => p.Orders).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_OrderDetails_OrderId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<OrderGroup>(entity =>
        {
            entity.HasIndex(e => e.ShipperId, "IX_OrderGroups_ShipperId").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Shipper).WithOne(p => p.OrderGroup).HasForeignKey<OrderGroup>(d => d.ShipperId);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<RecipeCategory>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_RecipeCategories_CategoryId");

            entity.HasIndex(e => e.RecipeId, "IX_RecipeCategories_RecipeId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Category).WithMany(p => p.RecipeCategories).HasForeignKey(d => d.CategoryId);

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeCategories).HasForeignKey(d => d.RecipeId);
        });

        modelBuilder.Entity<RecipeIngredient>(entity =>
        {
            entity.HasIndex(e => e.IngredientId, "IX_RecipeIngredients_IngredientId");

            entity.HasIndex(e => e.RecipeId, "IX_RecipeIngredients_RecipeId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Ingredient).WithMany(p => p.RecipeIngredients).HasForeignKey(d => d.IngredientId);

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeIngredients).HasForeignKey(d => d.RecipeId);
        });

        modelBuilder.Entity<RecipeIngredientOrderDetail>(entity =>
        {
            entity.HasIndex(e => e.OrderDetailId, "IX_RecipeIngredientOrderDetails_OrderDetailId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.OrderDetail).WithMany(p => p.RecipeIngredientOrderDetails).HasForeignKey(d => d.OrderDetailId);
        });

        modelBuilder.Entity<RecipeNutrient>(entity =>
        {
            entity.HasIndex(e => e.RecipeId, "IX_RecipeNutrients_RecipeID").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

            entity.HasOne(d => d.Recipe).WithOne(p => p.RecipeNutrient).HasForeignKey<RecipeNutrient>(d => d.RecipeId);
        });

        modelBuilder.Entity<RecipeStep>(entity =>
        {
            entity.HasIndex(e => e.RecipeId, "IX_RecipeSteps_RecipeId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.MediaUrl).HasColumnName("MediaURL");
            entity.Property(e => e.Name).HasDefaultValue("");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeSteps).HasForeignKey(d => d.RecipeId);
        });

        modelBuilder.Entity<RecipesPlan>(entity =>
        {
            entity.HasIndex(e => e.RecipeId, "IX_RecipesPlans_RecipeId");

            entity.HasIndex(e => e.StandardWeeklyPlanId, "IX_RecipesPlans_StandardWeeklyPlanId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipesPlans).HasForeignKey(d => d.RecipeId);

            entity.HasOne(d => d.StandardWeeklyPlan).WithMany(p => p.RecipesPlans).HasForeignKey(d => d.StandardWeeklyPlanId);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasIndex(e => e.OrderId, "IX_Transactions_OrderId");

            entity.HasOne(d => d.Order).WithMany(p => p.Transactions).HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<WeeklyPlan>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
