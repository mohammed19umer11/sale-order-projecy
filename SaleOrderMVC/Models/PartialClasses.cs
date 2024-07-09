using Microsoft.AspNetCore.Mvc;

namespace SaleOrderMVC.Models
{
    //[ModelMetadataType(typeof(EmployeeMetaData))]
    //public partial class Employee { }

    [ModelMetadataType(typeof(SaleOrderVM))]
    public partial class SaleOrderVM { }
}
