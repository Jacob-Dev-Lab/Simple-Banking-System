using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.ErrorHandler;
using SimpleBankingSystem.Presentation.Enums;
using SimpleBankingSystem.Presentation.Interface;

namespace SimpleBankingSystem.Presentation.Oprations
{
    internal class UpdateProfileHandler(IUserInputReader userInputReader,
        IConsoleRenderer consoleRenderer,
        ICustomerProfileService customerProfileService) : IAppStateHandler
    {
        private readonly IUserInputReader _userInputReader = userInputReader;
        private readonly IConsoleRenderer _consoleRenderer = consoleRenderer;
        private readonly ICustomerProfileService _customerProfileService = customerProfileService;

        public AppState State => AppState.UpdateCustomerProfile;

        public AppState Handle()
        {
            var accountNumber = _userInputReader.ReadAccountNumber("Enter account number: ");
            _consoleRenderer.ShowProfileUpdateMenu();

            var options = (CustomerDataType)_userInputReader.ReadInt("Select an option: ");
            Result result;

            switch (options)
            {
                case CustomerDataType.LastName:
                    var lastname = _userInputReader.ReadString("Enter new lastname: ");
                    result = _customerProfileService.UpdateLastName(accountNumber, lastname);
                    break;

                case CustomerDataType.Email:
                    var email = _userInputReader.ReadString("Enter new email address: ");
                    result = _customerProfileService.UpdateLastName(accountNumber, email);
                    break;

                default:
                    _consoleRenderer.ShowMessage("Invalid entry...");
                    return AppState.MainMenu;
            }
                
            _consoleRenderer.ShowMessage(result.Message ?? "Operation Failed");

            return AppState.MainMenu;
        }
    }
}
