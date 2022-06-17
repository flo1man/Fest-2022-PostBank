using SoftUniFest.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniFest.Data.Models.App
{
    public class AppPosTerminal : BaseDeletableModel<string>
    {
        public AppPosTerminal()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string TraderId { get; set; }

        public AppTrader Trader { get; set; }
    }
}
