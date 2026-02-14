using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBankingSystem.Domain.Interfaces;

namespace TestSimpleBankingSystem.Tests
{
    public class FakeAccountNumberGenerator : IGenerateAccountNumber
    {
        public string Generate()
        {
            return "12345";
        }
    }
}
