using System;
using UniRx;

namespace StickyTeam.FashionClash.Wallet.Core.Domain
{
    public interface WalletRepository
    {
        IObservable<PlayerWallet> Get();
        IObservable<Unit> Save();
    }
}