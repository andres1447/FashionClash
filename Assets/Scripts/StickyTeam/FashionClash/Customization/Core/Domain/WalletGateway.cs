using System;

namespace StickyTeam.FashionClash.Customization.Core.Domain
{
    public interface WalletGateway
    {
        IObservable<bool> CanPay(Price price);
        IObservable<bool> Pay(Price price);
    }
}