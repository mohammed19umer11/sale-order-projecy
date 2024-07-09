using System;
using System.Collections.Generic;

namespace SaleOrderMVC.Context;

public partial class SaleOrderDetail
{
    public int SaleorderdetailId { get; set; }

    public int SaleorderId { get; set; }

    public int ItemId { get; set; }

    public int ItemQty { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual SaleOrder Saleorder { get; set; } = null!;
}
