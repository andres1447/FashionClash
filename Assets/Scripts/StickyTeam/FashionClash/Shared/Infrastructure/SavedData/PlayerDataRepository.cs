using System;
using System.Collections.Generic;
using UniRx;

namespace StickyTeam.FashionClash.Shared.Core
{
    public interface PlayerDataRepository
    {
        IObservable<List<PlayerDataCurrency>> GetWallet();
        IObservable<Unit> Save(List<PlayerDataCurrency> wallet);
        IObservable<Unit> AddPurchasedItem(string itemId);
        IObservable<List<string>> GetPurchasedItems();
        IObservable<int> GetLevel();
    }
}