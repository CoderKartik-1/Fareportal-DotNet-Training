using System;
using System.Collections.Generic;

namespace BankProjectEfCore.Models;

public partial class KartikSbaccount
{
    public int AccountNumber { get; set; }

    public string? CustomerName { get; set; }

    public string? CustomerAddress { get; set; }

    public decimal? CurrentBalance { get; set; }

    public virtual ICollection<KartikSbtransaction> KartikSbtransactions { get; set; } = new List<KartikSbtransaction>();
}
