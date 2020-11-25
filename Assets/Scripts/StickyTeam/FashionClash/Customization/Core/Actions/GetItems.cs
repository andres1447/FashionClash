using System;
using System.Collections.Generic;
using System.Linq;
using StickyTeam.FashionClash.Customization.Core.Domain;
using StickyTeam.FashionClash.Customization.Core.Gateways;
using UniRx;

namespace StickyTeam.FashionClash.Customization.Core.Actions
{
    public class GetItems
    {
        private readonly PlayerProgressGateway _playerProgressGateway;
        private readonly PurchasedItemsGateway _purchasedItemsGateway;
        private readonly WalletGateway _walletGateway;

        public GetItems()
        {
        }

        public GetItems(
            PlayerProgressGateway playerProgressGateway,
            PurchasedItemsGateway purchasedItemsGateway,
            WalletGateway walletGateway)
        {
            _playerProgressGateway = playerProgressGateway;
            _purchasedItemsGateway = purchasedItemsGateway;
            _walletGateway = walletGateway;
        }

        public virtual IObservable<List<Item>> Execute(Category category)
        {
            return Observable.Return(category.Items.Select(Map).ToList());
        }

        private Item Map(ItemDetail it)
        {
            var isPurchased = _purchasedItemsGateway.IsPurchased(it);
            return new Item(it)
            {
                IsPurchased = isPurchased,
                CanPurchase = !isPurchased && _walletGateway.CanPay(it.Price).Wait(),
                IsUnlocked = _playerProgressGateway.GetLevel() >= it.RequiredLevel
            };
        }
    }
}