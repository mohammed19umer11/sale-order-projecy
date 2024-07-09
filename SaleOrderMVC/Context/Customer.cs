using System;
using System.Collections.Generic;

namespace SaleOrderMVC.Context;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerAddress { get; set; } = null!;

    public string CustomerContact { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public virtual ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
}
