using System;
using System.Collections.Generic;
using System.Linq;
using StickyTeam.FashionClash.Shared.Core;
using UniRx;

namespace StickyTeam.FashionClash.Shared.Infrastructure.SavedData
{
    public class InMemoryPlayerDataRepository : PlayerDataRepository
    {
        private PlayerData _data = new PlayerData();

        public IObservable<List<PlayerDataCurrency>> GetWallet()
        {
            return Observable.Return(_data.Wallet);
        }

        public IObservable<Unit> Save(List<PlayerDataCurrency> wallet)
        {
            _data.Wallet = wallet;
            return Observable.ReturnUnit();
        }

        public IObservable<Unit> AddPurchasedItem(string itemId)
        {
            _data.PurchasedItems.Add(itemId);
            return Observable.ReturnUnit();
        }

        public IObservable<List<string>> GetPurchasedItems()
        {
            return Observable.Return(_data.PurchasedItems);
        }

        public IObservable<int> GetLevel()
        {
            return Observable.Return(_data.Level);
        }
    }
}