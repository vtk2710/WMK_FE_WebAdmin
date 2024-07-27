using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public int EmailConfirm { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public int Gender { get; set; }

    public string? Address { get; set; }

    public string? Code { get; set; }

    public int Role { get; set; }

    public int AccessFailedCount { get; set; }

    public int Status { get; set; }

    public virtual OrderGroup? OrderGroup { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
