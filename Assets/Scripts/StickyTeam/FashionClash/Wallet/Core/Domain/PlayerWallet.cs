using System;
using System.Collections.Generic;
using System.Linq;
using StickyTeam.FashionClash.Shared.Core;

namespace StickyTeam.FashionClash.Wallet.Core.Domain
{
    public class PlayerWallet
    {
        private Dictionary<Currency, int> _quantities = new Dictionary<Currency, int>();

        public List<CurrencyAmount> Amounts => _quantities.Select(ca => new CurrencyAmount(ca.Key, ca.Value)).ToList();

        public PlayerWallet()
        {
        }

        public PlayerWallet(List<CurrencyAmount> amounts)
        {
            _quantities = amounts.ToDictionary(x => x.Currency, x => x.Amount);
        }

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

        public bool CanPay(Currency currency, int amount)
        {
            return Amount(currency) >= amount;
        }
    }
}