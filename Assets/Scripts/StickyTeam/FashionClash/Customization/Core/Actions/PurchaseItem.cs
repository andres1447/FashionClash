using System;
using StickyTeam.FashionClash.Customization.Core.Domain;
using UniRx;

namespace StickyTeam.FashionClash.Customization.Core.Actions
{
    public class PurchaseItem
    {
        private readonly WalletGateway _wallet;
        
        public PurchaseItem() { }

        public PurchaseItem(WalletGateway wallet)
        {
            _wallet = wallet;
        }

        public virtual IObservable<bool> Execute(Item item)
        {
            return _wallet.Pay(item)
                .Where(success => success)
                .Do(_ => ProcessPurchase(item));
        }

        private void ProcessPurchase(Item item)
        {
            item.Purchase();
        }
    }
}