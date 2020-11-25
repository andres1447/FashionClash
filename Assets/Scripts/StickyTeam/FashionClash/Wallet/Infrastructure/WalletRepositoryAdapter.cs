using System;
using System.Collections.Generic;
using System.Linq;
using StickyTeam.FashionClash.Shared.Core;
using StickyTeam.FashionClash.Wallet.Core.Domain;
using UniRx;

namespace StickyTeam.FashionClash.Wallet.Infrastructure
{
    public class WalletRepositoryAdapter : WalletRepository
    {
        private List<Currency> _currencies;
        private PlayerDataRepository _playerDataRepository;
        private PlayerWallet _wallet;
        
        public WalletRepositoryAdapter(List<Currency> currencies, PlayerDataRepository playerDataRepository)
        {
            _currencies = currencies;
            _playerDataRepository = playerDataRepository;
        }

        public IObservable<PlayerWallet> Get()
        {
            var currencies = _playerDataRepository.GetWallet().Wait();
            return Observable.Return(_wallet ?? (_wallet = MapWallet(currencies)));
        }

        private PlayerWallet MapWallet(IEnumerable<PlayerDataCurrency> currencies)
        {
            return new PlayerWallet(currencies.Select(MapPlayerDataCurrency).ToList());
        }

        private CurrencyAmount MapPlayerDataCurrency(PlayerDataCurrency data)
        {
            var currency = _currencies.First(c => c.Id == data.CurrencyId);
            return new CurrencyAmount(currency, data.Amount);
        }

        public IObservable<Unit> Save()
        {
            return _playerDataRepository.Save(ToPlayerDataCurrency(_wallet.Amounts));
        }

        private List<PlayerDataCurrency> ToPlayerDataCurrency(List<CurrencyAmount> walletAmounts)
        {
            return walletAmounts.Select(x => new PlayerDataCurrency(x.Currency.Id, x.Amount)).ToList();
        }
    }
}