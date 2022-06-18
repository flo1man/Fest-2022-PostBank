namespace SoftUniFest.Services
{
    using System.Linq;

    using SoftUniFest.Data;

    public class ApproveEmployeeService : IApproveEmployeeService
    {
        private readonly ApplicationDbContext2 dbContext;

        public ApproveEmployeeService(ApplicationDbContext2 dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool IsApproved(string discountId, string userId)
        {
            return this.dbContext.ApproveDiscounts
                .Any(x => x.DiscountId == discountId && x.EmployeeId == userId);
        }
    }
}
