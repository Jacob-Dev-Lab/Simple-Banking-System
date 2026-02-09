using Microsoft.Extensions.DependencyInjection;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Application.Service;
using SimpleBankingSystem.Application.Service.AccountService;
using SimpleBankingSystem.Application.Service.CustomerService;
using SimpleBankingSystem.Domain.Entities;
using SimpleBankingSystem.Infrastructure.Repositories;
using SimpleBankingSystem.Infrastructure.Service;
using SimpleBankingSystem.Presentation;
using SimpleBankingSystem.Utilities;

namespace SimpleBankingSystem
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Set up dependency injection for the application services, repositories, and utilities
            var service = new ServiceCollection();

            service.AddSingleton<IFileConnection, FileConnection>();
            service.AddSingleton<ILogger, Logger>();
            service.AddSingleton<IGenerateAccountNumber, GuidAccountNumber>();

            service.AddSingleton<ICustomerRepository, FileCustomerRepository>();
            service.AddSingleton<IAccountRepository, FileAccountRepository>();
            service.AddSingleton<ITransactionRepository, FileTransactionRepository>();

            service.AddTransient<IAccountOpeningService, AccountOpeningService>();
            service.AddTransient<IAccountOperationService, AccountOperationService>();
            service.AddTransient<IAccountQueryService, AccountQueryService>();
            service.AddTransient<IAccountStatusService, AccountStatusService>();
            service.AddTransient<ICustomerProfileService, CustomerProfileService>();

            service.AddTransient<IBankApp, BankApp>();

            // Build the service provider and load data from the repositories before running the application
            var serviceProvider = service.BuildServiceProvider();

            serviceProvider.GetService<ICustomerRepository>()?.Load();
            serviceProvider.GetService<IAccountRepository>()?.Load();
            serviceProvider.GetService<ITransactionRepository>()?.Load();

            // Run the main application
            var app = serviceProvider.GetService<IBankApp>();
            app?.Run();
        }
    }
}