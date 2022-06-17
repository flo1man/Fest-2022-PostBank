namespace SoftUniFest.Services.CronJobs
{
    using SoftUniFest.Data;
    using SoftUniFest.Data.Common;
    using SoftUniFest.Data.Common.Repositories;
    using SoftUniFest.Data.Models;
    using SoftUniFest.Data.Models.App;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PosTerminalSync
    {
        private readonly IDbQueryRunner queryRunner;
        private readonly IDeletableEntityRepository<POSTerminal> terminalRepository;
        private readonly ApplicationDbContext2 dbContext;

        public PosTerminalSync(IDbQueryRunner queryRunner,
            IDeletableEntityRepository<POSTerminal> terminalRepository,
            ApplicationDbContext2 dbContext)
        {
            this.queryRunner = queryRunner;
            this.terminalRepository = terminalRepository;
            this.dbContext = dbContext;
        }

        public async Task Work()
        {
            await queryRunner.RunQueryAsync(
                $"TRUNCATE TABLE [SoftUniFest2].[dbo].[PosTerminals]");

            var traders = this.terminalRepository.All().ToList();

            foreach (var trader in traders)
            {
                await dbContext.PosTerminals.AddAsync(new AppPosTerminal
                {
                    Id = trader.Id,
                    TraderId = trader.TraderId,
                });
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
