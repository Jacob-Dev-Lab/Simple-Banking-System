using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankingSystem.Presentation.Enums
{
    public enum AppState
    {
        MainMenu,
        OpenNewAccount,
        OpenAdditionalAccount,
        MakeDeposit,
        MakeWithdrawal,
        CheckAccountBalance,
        PrintAccountStatement,
        UpdateCustomerProfile,
        ChangeAccountStatus,
        Exit
    }
}
