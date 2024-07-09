using System;
using System.Collections.Generic;

namespace SaleOrderMVC.Context;

public partial class SaleOrder
{
    public int SaleorderId { get; set; }

    public int CustomerId { get; set; }

    public int EmployeeId { get; set; }

    public int OrderstatusId { get; set; }

    public DateTime Createdate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual OrderStatus Orderstatus { get; set; } = null!;

    public virtual ICollection<SaleOrderDetail> SaleOrderDetails { get; set; } = new List<SaleOrderDetail>();
}
