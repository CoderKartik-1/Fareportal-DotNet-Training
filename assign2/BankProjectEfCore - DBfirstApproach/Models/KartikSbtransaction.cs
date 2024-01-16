using System;
using System.Collections.Generic;

namespace BankProjectEfCore.Models;

public partial class KartikSbtransaction
{
    public int TransactionId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public int AccountNumber { get; set; }

    public decimal? Amount { get; set; }

    public string? TransactionType { get; set; }

    public virtual KartikSbaccount AccountNumberNavigation { get; set; } = null!;
}
