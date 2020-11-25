using System;

namespace StickyTeam.FashionClash.Shared.Core
{
    [Serializable]
    public class CurrencyAmount
    {
        public Currency Currency { get; private set; }
        public int Amount { get; private set; }
        
        public CurrencyAmount(Currency currency, int amount)
        {
            Currency = currency;
            Amount = amount;
        }
    }
}