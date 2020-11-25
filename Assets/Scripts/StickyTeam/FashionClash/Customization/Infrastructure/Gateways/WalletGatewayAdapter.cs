using System;
using StickyTeam.FashionClash.Customization.Core.Domain;
using StickyTeam.FashionClash.Customization.Core.Gateways;
using StickyTeam.FashionClash.Wallet.Core.Service;

namespace StickyTeam.FashionClash.Customization.Infrastructure.Gateways
{
    public class WalletGatewayAdapter : WalletGateway
    {
        private WalletService _walletService;

        public WalletGatewayAdapter(WalletService walletService)
        {
            _walletService = walletService;
        }
        
        public IObservable<bool> CanPay(Price price)
        {
            return _walletService.CanPay(price.Currency, price.Amount);
        }

        public IObservable<bool> Pay(Price price)
        {
            return _walletService.Take(price.Currency, price.Amount);
        }
    }
}