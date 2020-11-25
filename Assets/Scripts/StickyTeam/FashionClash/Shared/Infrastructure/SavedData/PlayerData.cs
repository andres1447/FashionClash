using System;
using System.Collections.Generic;

namespace StickyTeam.FashionClash.Shared.Core
{
    [Serializable]
    public class PlayerData
    {
        public int Level { get; set; }
        public List<PlayerDataCurrency> Wallet { get; set; } = new List<PlayerDataCurrency>();
        public List<string> PurchasedItems { get; set; } = new List<string>();
        public DateTime CreationDateTime { get; set; }
        public DateTime LastOpenDateTime { get; set; }
    }

    [Serializable]
    public class PlayerDataCurrency
    {
        public string CurrencyId { get; set; }
        public int Amount { get; set; }

        public PlayerDataCurrency()
        {
        }

        public PlayerDataCurrency(string currencyId, int amount)
        {
            CurrencyId = currencyId;
            Amount = amount;
        }
    }
}