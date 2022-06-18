namespace SoftUniFest.Web.ViewModels.Discounts
{
    using System;

    using SoftUniFest.Data.Models.App;
    using SoftUniFest.Services.Mapping;

    public class WaitingDiscountsViewModel : IMapFrom<Discount>
    {
        public string TraderUsername { get; set; }

        public int Percentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
