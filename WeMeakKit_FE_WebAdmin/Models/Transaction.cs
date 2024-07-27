using System;
using System.Collections.Generic;

namespace WeMeakKit_FE_WebAdmin.Models;

public partial class Transaction
{
    public string Id { get; set; } = null!;

    public Guid OrderId { get; set; }

    public int Type { get; set; }

    public double Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? Notice { get; set; }

    public string? ExtraData { get; set; }

    public string? Signature { get; set; }

    public int Status { get; set; }

    public virtual Order Order { get; set; } = null!;
}
