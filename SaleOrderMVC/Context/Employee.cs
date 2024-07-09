using System;
using System.Collections.Generic;

namespace SaleOrderMVC.Context;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string EmployeeFirstname { get; set; } = null!;

    public string EmployeeLastname { get; set; } = null!;

    public string EmployeeEmail { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public virtual ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
}
