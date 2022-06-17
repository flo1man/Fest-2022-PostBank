namespace SoftUniFest.Data.Models.App
{
    using System;

    public class AppPosTerminal
    {
        public AppPosTerminal()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string TraderId { get; set; }

        public AppTrader Trader { get; set; }
    }
}
