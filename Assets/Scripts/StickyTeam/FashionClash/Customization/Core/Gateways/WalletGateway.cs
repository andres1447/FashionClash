using System;
using StickyTeam.FashionClash.Customization.Core.Domain;

namespace StickyTeam.FashionClash.Customization.Core.Gateways
{
    public interface WalletGateway
    {
        IObservable<bool> CanPay(Price price);
        IObservable<bool> Pay(Price price);
    }
}