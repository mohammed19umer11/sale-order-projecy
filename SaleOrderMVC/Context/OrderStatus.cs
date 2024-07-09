using System;
using System.Collections.Generic;

namespace SaleOrderMVC.Context;

public partial class OrderStatus
{
    public int OrderstatusId { get; set; }

    public string OrderstatusName { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public virtual ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
}
