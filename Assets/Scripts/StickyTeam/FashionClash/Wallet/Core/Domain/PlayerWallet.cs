using System;
using System.Collections.Generic;
using StickyTeam.FashionClash.Shared.Core;

namespace StickyTeam.FashionClash.Wallet.Core.Domain
{
    public class PlayerWallet
    {
        private Dictionary<Currency, int> _quantities = new Dictionary<Currency, int>();

        public void Add(Currency currency, int amount)
        {
            _quantities.Add(currency, amount);
        }

        private int Amount(Currency currency)
        {
            return _quantities.TryGetValue(currency, out var current) ? current : 0;
        }

        public bool Take(Currency currency, int amount)
        {
            var current = Amount(currency);
            if (current < amount)
                return false;

            _quantities[currency] = current - amount;
            return true;
        }
    }
}