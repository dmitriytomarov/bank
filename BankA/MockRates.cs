using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA
{
    internal class MockRates : IRateProvider
    {
        public decimal GetCurrentDepositRate()
        {
            return 0.035m;
        }
    }
}
