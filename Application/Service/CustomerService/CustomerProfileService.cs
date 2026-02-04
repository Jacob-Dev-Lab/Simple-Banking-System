using System.Net.Mail;
using SimpleBankingSystem.Application.Interfaces;
using SimpleBankingSystem.Domain.ErrorHandler;
using SimpleBankingSystem.Presentation;

namespace SimpleBankingSystem.Application.Service.CustomerService
{
    internal class CustomerProfileService(ICustomerRepository customerRepository) : ICustomerProfileService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public Result UpdateLastName(Guid customerID, string lastname)
        {
            var customer = _customerRepository.GetCustomerById(customerID);

            var result = customer.ChangeLastname(lastname);

            if (result.IsFailure) 
                return result;

            _customerRepository.Save();
            return Result.Success();
        }
        public Result UpdateEmailAddress(Guid customerID, string emailAddress)
        {
            var customer = _customerRepository.GetCustomerById(customerID);

            var result = customer.ChangeEmailAddress(emailAddress);

            if (result.IsFailure) 
                return result;

            _customerRepository.Save();
            return Result.Success();
        }
    }
}
