using static BankA.Account;

namespace BankA
{
    internal class MockRates : IRateProvider
    {
        public decimal GetCurrentDepositRate(Currency cur)
        {
            switch (cur)
            {
                case Currency.EUR:
                    return 0.005m;
                case Currency.RUR:
                    return 0.035m;
                case Currency.USD:
                    return 0.004m;
                case Currency.CNY:
                    return 0.002m;
            }
            return 0m;
        }
    }
}
