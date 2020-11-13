using System;

namespace StickyTeam.FashionClash.Wallet.Core.Domain
{
    public interface WalletRepository
    {
        IObservable<PlayerWallet> Get();
    }
}