namespace SoftUniFest.Data.Models.App
{
    using System;

    public class Discount
    {
        public Discount()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string TraderId { get; set; }

        public AppTrader Trader { get; set; }

        public int Percentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public StatusType Status { get; set; }

        public int ApproveCount { get; set; }
    }
}
