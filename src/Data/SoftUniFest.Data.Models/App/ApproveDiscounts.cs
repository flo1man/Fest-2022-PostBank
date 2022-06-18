namespace SoftUniFest.Data.Models.App
{
    using System;

    public class ApproveDiscounts
    {
        public ApproveDiscounts()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string DiscountId { get; set; }

        public string EmployeeId { get; set; }
    }
}
