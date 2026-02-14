using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Application.Service.AccountService;
using SimpleBankingSystem.Application.Service.CustomerService;
using SimpleBankingSystem.Domain.Interfaces;
using SimpleBankingSystem.Infrastructure.Interface;
using SimpleBankingSystem.Infrastructure.Repositories;
using SimpleBankingSystem.Infrastructure.Service;
using SimpleBankingSystem.Presentation;
using SimpleBankingSystem.Presentation.ConsoleUI;
using SimpleBankingSystem.Presentation.Interface;
using SimpleBankingSystem.Presentation.Oprations;

namespace SimpleBankingSystem
{
    class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IFileConnection, FileConnection>();
                    
                    services.AddSingleton<IGenerateAccountNumber, GuidAccountNumber>();

                    services.AddSingleton<ICustomerRepository, FileCustomerRepository>();
                    services.AddSingleton<IAccountRepository, FileAccountRepository>();
                    services.AddSingleton<ITransactionRepository, FileTransactionRepository>();

                    services.AddTransient<ICreateCustomerService, CreateCustomerService>();
                    services.AddTransient<IAccountOpeningService, AccountOpeningService>();
                    services.AddTransient<IAccountOperationService, AccountOperationService>();
                    services.AddTransient<IAccountQueryService, AccountQueryService>();
                    services.AddTransient<IAccountStatusService, AccountStatusService>();
                    services.AddTransient<ICustomerProfileService, CustomerProfileService>();

                    services.AddSingleton<IConsoleRenderer, ConsoleRenderer>();
                    services.AddSingleton<IUserInputReader, UserInputReader>();

                    services.AddTransient<IAppStateHandler, MainMenuHandler>();
                    services.AddTransient<IAppStateHandler, NewAccountHandler>();
                    services.AddTransient<IAppStateHandler, AdditionalAccountHandler>();
                    services.AddTransient<IAppStateHandler, DepositHandler>();
                    services.AddTransient<IAppStateHandler, WithdrawalHandler>();
                    services.AddTransient<IAppStateHandler, AccountBalanceHandler>();
                    services.AddTransient<IAppStateHandler, AccountStatementHandler>();
                    services.AddTransient<IAppStateHandler, AccountStatusHandler>();
                    services.AddTransient<IAppStateHandler, UpdateProfileHandler>();

                    services.AddTransient<IBankApp, BankApp>();
                })
                .UseSerilog((context, services, configuration) =>
                {
                    var connection = services.GetRequiredService<IFileConnection>();

                    configuration
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .WriteTo.File(connection.LogFilePath);
                })
                .Build();

            host.Services.GetRequiredService<ICustomerRepository>()?.Load();
            host.Services.GetRequiredService<IAccountRepository>()?.Load();
            host.Services.GetRequiredService<ITransactionRepository>()?.Load();

            var app = host.Services.GetRequiredService<IBankApp>();
            Log.Information("Starting the Simple Banking System application.");
            app.Run();
        }
    }
}