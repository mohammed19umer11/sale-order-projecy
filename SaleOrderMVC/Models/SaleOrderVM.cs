using SaleOrderMVC.Context;
using System.ComponentModel.DataAnnotations;

namespace SaleOrderMVC.Models
{
    public partial class SaleOrderVM
    {
        public int SaleorderId { get; set; }

        public int CustomerId { get; set; }

        public int EmployeeId { get; set; }

        public int OrderstatusId { get; set; }

        public int ItemId { get; set; }

        public int ItemQty { get; set; }

        public Customer Customer { get; set; } = null!;

        public Employee Employee { get; set; } = null!;

        public OrderStatus Orderstatus { get; set; } = null!;

        public Item Item { get; set; } = null!;

        public DateTime Createdate { get; set; }
    }
}
