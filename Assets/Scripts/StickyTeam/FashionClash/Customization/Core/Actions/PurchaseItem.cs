using System;
using StickyTeam.FashionClash.Customization.Core.Domain;
using StickyTeam.FashionClash.Customization.Core.Gateways;
using UniRx;

namespace StickyTeam.FashionClash.Customization.Core.Actions
{
    public class PurchaseItem
    {
        private readonly WalletGateway _wallet;
        private readonly PurchasedItemsGateway _purchasedItemsGateway;
        
        public PurchaseItem() { }

        public PurchaseItem(WalletGateway wallet, PurchasedItemsGateway purchasedItemsGateway)
        {
            _wallet = wallet;
            _purchasedItemsGateway = purchasedItemsGateway;
        }

        public virtual IObservable<bool> Execute(ItemDetail item)
        {
            return _wallet.Pay(item.Price)
                .Where(success => success)
                .SelectMany(_ => ProcessPurchase(item)
                .Select(__ => true));
        }

        private IObservable<Unit> ProcessPurchase(ItemDetail item)
        {
            return _purchasedItemsGateway.Add(item);
        }
    }
}