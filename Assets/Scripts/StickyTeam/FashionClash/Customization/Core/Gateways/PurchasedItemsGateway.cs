using System;
using StickyTeam.FashionClash.Customization.Core.Domain;
using UniRx;

namespace StickyTeam.FashionClash.Customization.Core.Gateways
{
    public interface PurchasedItemsGateway
    {
        IObservable<Unit> Add(ItemDetail item);
        bool IsPurchased(ItemDetail item);
    }
}