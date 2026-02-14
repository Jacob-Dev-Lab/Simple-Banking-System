using SimpleBankingSystem.Presentation.Enums;

namespace SimpleBankingSystem.Presentation.Interface
{
    public interface IAppStateHandler
    {
        AppState State { get; }
        AppState Handle();
    }
}
