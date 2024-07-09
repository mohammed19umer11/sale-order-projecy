using System;
using System.Collections.Generic;

namespace SaleOrderMVC.Context;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public string? ItemDescription { get; set; }

    public int ItemQuantity { get; set; }

    public decimal ItemPrice { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<SaleOrderDetail> SaleOrderDetails { get; set; } = new List<SaleOrderDetail>();
}
