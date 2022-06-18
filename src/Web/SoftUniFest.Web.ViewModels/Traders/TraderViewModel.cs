namespace SoftUniFest.Web.ViewModels.Traders
{
    using System;
    using System.Collections.Generic;

    using SoftUniFest.Data.Models.App;
    using SoftUniFest.Services.Mapping;

    public class TraderViewModel : IMapFrom<AppTrader>
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfRegister { get; set; }

        public ICollection<Discount> Discounts { get; set; }

        public ICollection<AppPosTerminal> PosTerminals { get; set; }
    }
}
