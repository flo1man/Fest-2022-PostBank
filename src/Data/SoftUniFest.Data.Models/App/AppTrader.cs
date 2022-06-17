namespace SoftUniFest.Data.Models.App
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoftUniFest.Data.Common.Models;

    public class AppTrader : BaseDeletableModel<string>
    {
        public AppTrader()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Discounts = new HashSet<Discount>();
            this.PosTerminals = new HashSet<AppPosTerminal>();
        }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public ICollection<Discount> Discounts { get; set; }

        public ICollection<AppPosTerminal> PosTerminals { get; set; }
    }
}
