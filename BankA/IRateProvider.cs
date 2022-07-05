using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA
{
    internal interface IRateProvider
    {
        public decimal GetCurrentDepositRate();
    }
}
