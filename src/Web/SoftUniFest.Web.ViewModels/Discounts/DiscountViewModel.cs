namespace SoftUniFest.Web.ViewModels.Discounts
{
    using System;

    using SoftUniFest.Data.Models.App;
    using SoftUniFest.Services.Mapping;

    public class DiscountViewModel : IMapFrom<Discount>
    {
        public string Id { get; set; }

        public int Percentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public StatusType Status { get; set; }
    }
}
