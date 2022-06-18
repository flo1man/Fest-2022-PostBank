namespace SoftUniFest.Services
{
    public interface IApproveEmployeeService
    {
        bool IsApproved(string discountId, string userId);
    }
}
