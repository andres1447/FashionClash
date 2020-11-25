using System;
using StickyTeam.FashionClash.Shared.Core;
using StickyTeam.FashionClash.Wallet.Core.Domain;
using UniRx;

namespace StickyTeam.FashionClash.Wallet.Core.Service
{
    public class WalletService
    {
        private readonly WalletRepository _repository;

        public WalletService()
        {
        }
        
        public WalletService(WalletRepository repository)
        {
            _repository = repository;
        }

        public virtual IObservable<Unit> Add(Currency currency, int amount)
        {
            return _repository.Get()
                .Do(wallet => wallet.Add(currency, amount))
                .SelectMany(wallet => _repository.Save())
                .Take(1);
        }

        public virtual IObservable<bool> Take(Currency currency, int amount)
        {
            return _repository.Get()
                .Select(wallet => wallet.Take(currency, amount))
                .Where(success => success)
                .Do(_ => _repository.Save())
                .Take(1);
        }

        public IObservable<bool> CanPay(Currency currency, int amount)
        {
            return _repository.Get().Select(wallet => wallet.CanPay(currency, amount));
        }
    }
}