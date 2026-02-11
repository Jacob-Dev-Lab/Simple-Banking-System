using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Application.Service;
using SimpleBankingSystem.Application.Service.AccountService;
using SimpleBankingSystem.Application.Service.CustomerService;
using SimpleBankingSystem.Infrastructure.Repositories;
using SimpleBankingSystem.Infrastructure.Service;
using SimpleBankingSystem.Presentation;

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

                    services.AddTransient<IAccountOpeningService, AccountOpeningService>();
                    services.AddTransient<IAccountOperationService, AccountOperationService>();
                    services.AddTransient<IAccountQueryService, AccountQueryService>();
                    services.AddTransient<IAccountStatusService, AccountStatusService>();
                    services.AddTransient<ICustomerProfileService, CustomerProfileService>();

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

            //// Set up dependency injection for the application services, repositories, and utilities
            //var services = new ServiceCollection();

            //services.AddSingleton<IFileConnection, FileConnection>();
            
            ////services.AddSingleton<ILogger, Logger>();
            //services.AddSingleton<IGenerateAccountNumber, GuidAccountNumber>();

            //services.AddSingleton<ICustomerRepository, FileCustomerRepository>();
            //services.AddSingleton<IAccountRepository, FileAccountRepository>();
            //services.AddSingleton<ITransactionRepository, FileTransactionRepository>();

            //services.AddTransient<IAccountOpeningService, AccountOpeningService>();
            //services.AddTransient<IAccountOperationService, AccountOperationService>();
            //services.AddTransient<IAccountQueryService, AccountQueryService>();
            //services.AddTransient<IAccountStatusService, AccountStatusService>();
            //services.AddTransient<ICustomerProfileService, CustomerProfileService>();

            //services.AddTransient<IBankApp, BankApp>();

           
                

            //// Build the service provider and load data from the repositories before running the application
            //var serviceProvider = services.BuildServiceProvider();

            //serviceProvider.GetService<ICustomerRepository>()?.Load();
            //serviceProvider.GetService<IAccountRepository>()?.Load();
            //serviceProvider.GetService<ITransactionRepository>()?.Load();

            //// Run the main application
            //var app = serviceProvider.GetService<IBankApp>();
            //app?.Run();
        }
    }
}