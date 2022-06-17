using SoftUniFest.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniFest.Data.Models.App
{
    public class Discount : BaseDeletableModel<string>
    {
        public Discount()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string TraderId { get; set; }

        public AppTrader Trader { get; set; }

        public int Percentage { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public StatusType Status { get; set; }
    }
}
