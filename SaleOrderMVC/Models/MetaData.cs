
using System.ComponentModel.DataAnnotations;

namespace SaleOrderMVC.Models
{
    //public class EmployeeMetaData
    //{
    //    [Required(ErrorMessage = "Please  enter employee first name")]
    //    public string EmployeeFirstname { get; set; } = null!;

    //    [Required(ErrorMessage = "Please  enter employee last name")]
    //    public string EmployeeLastname { get; set; } = null!;

    //    public string FullName => $"{EmployeeFirstname} {EmployeeLastname}";

    //}

    public class SaleOrderVMMetaData
    {
        [Required(ErrorMessage = "Please select customer")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please select employee")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Please select order status")]
        public int OrderstatusId { get; set; }

        [Required(ErrorMessage = "Please select item")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Please enter quantity")]
        public int ItemQty { get; set; }
    }
}
