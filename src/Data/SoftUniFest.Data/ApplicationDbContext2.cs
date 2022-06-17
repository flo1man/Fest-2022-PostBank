namespace SoftUniFest.Data
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using SoftUniFest.Data.Common.Models;
    using SoftUniFest.Data.Models;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SoftUniFest.Data.Models.App;

    public class ApplicationDbContext2 : DbContext
    {
        public ApplicationDbContext2(DbContextOptions<ApplicationDbContext2> options)
            : base(options)
        {
        }

        public DbSet<AppTrader> Traders { get; set; }

        public DbSet<AppEmployee> Employees { get; set; }

        public DbSet<CardHolder> CardHolders { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        public DbSet<AppPosTerminal> PosTerminals { get; set; }
    }
}
