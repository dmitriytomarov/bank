using static BankA.Account;

namespace BankA
{
    internal interface IRateProvider
    {
        public decimal GetCurrentDepositRate(Currency cur);
    }
}
