using System;

namespace StickyTeam.FashionClash.Customization.Core.Domain
{
    public interface WalletGateway
    {
        IObservable<bool> CanPay(Item item);
        IObservable<bool> Pay(Item item);
    }
}