namespace SoftUniFest.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoftUniFest.Data.Common.Models;

    public class POSTerminal : BaseDeletableModel<string>
    {
        public POSTerminal()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string TraderId { get; set; }

        public virtual ApplicationUser Trader { get; set; }
    }
}
