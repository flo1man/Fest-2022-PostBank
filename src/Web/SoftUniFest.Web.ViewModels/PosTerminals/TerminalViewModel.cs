namespace SoftUniFest.Web.ViewModels.PosTerminals
{
    using SoftUniFest.Data.Models.App;
    using SoftUniFest.Services.Mapping;

    public class TerminalViewModel : IMapFrom<AppPosTerminal>
    {
        public string Id { get; set; }

        public string TraderUserName { get; set; }
    }
}
