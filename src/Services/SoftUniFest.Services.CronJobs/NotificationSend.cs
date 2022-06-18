namespace SoftUniFest.Services.CronJobs
{
    using SoftUniFest.Common;
    using SoftUniFest.Data;
    using SoftUniFest.Services.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NotificationSend
    {
        private readonly IEmailSender emailSender;
        private readonly ApplicationDbContext2 dbContext;

        public NotificationSend(
            IEmailSender emailSender,
            ApplicationDbContext2 dbContext)
        {
            this.emailSender = emailSender;
            this.dbContext = dbContext;
        }

        public async Task Work()
        {
            var discounts = this.dbContext.Discounts
                .Where(x => x.ApproveCount == 2)
                .Select(x => new
                {
                    Trader = x.Trader.Username,
                    Percentage = x.Percentage,
                    x.EndDate,
                })
                .ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<h1>Weekly Discounts:</h1>");

            foreach (var discount in discounts)
            {
                sb.AppendLine($"<h5>Trader:{discount.Trader}, Percentage: {discount.Percentage}%, Until: {discount.EndDate}</h5>");
            }

            await this.emailSender.SendEmailAsync(GlobalConstants.SiteEmail, GlobalConstants.SystemName, "skvproject@abv.bg", "Weekly Discounts", sb.ToString().Trim());
        }
    }
}
