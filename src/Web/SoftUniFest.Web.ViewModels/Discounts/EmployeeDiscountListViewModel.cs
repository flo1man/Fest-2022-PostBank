namespace SoftUniFest.Web.ViewModels.Discounts
{
    using System.Collections.Generic;

    using SoftUniFest.Data.Models.App;

    public class EmployeeDiscountListViewModel
    {
        public ICollection<Discount> Discounts { get; set; }

        public bool IsTurnOn { get; set; }
    }
}
