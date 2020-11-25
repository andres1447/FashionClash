using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using StickyTeam.FashionClash.Shared.Core;
using StickyTeam.FashionClash.Shared.Infrastructure.SavedData;
using StickyTeam.FashionClash.Wallet.Infrastructure;
using UniRx;

namespace StickyTeam.FashionClash.Wallet.Test
{
    public class WalletRepositoryAdapterShould
    {
        private const string CURRENCY_ID = "1";
        
        private readonly Currency CURRENCY = GivenAnCurrency();

        const int AMOUNT = 10;
            
        private PlayerDataRepository _playerDataRepository;
        private WalletRepositoryAdapter _walletRepository;
        
        [SetUp]
        public void SetUp()
        {
            _playerDataRepository = new InMemoryPlayerDataRepository();
            _walletRepository = new WalletRepositoryAdapter(new List<Currency> { CURRENCY }, _playerDataRepository);
        }

        [Test]
        public void save_amounts_to_player_data()
        {
            var wallet = _walletRepository.Get().Wait();
            wallet.Add(CURRENCY, AMOUNT);

            _walletRepository.Save().Wait();

            ThenPlayerHas(CURRENCY, AMOUNT);
        }

        private void ThenPlayerHas(Currency currency, int amount)
        {
            var currencies = _playerDataRepository.GetWallet().Wait();
            Assert.IsTrue(currencies.Any(x => x.CurrencyId == CURRENCY_ID && x.Amount == amount));
        }

        private static Currency GivenAnCurrency()
        {
            var currency = Substitute.For<Currency>();
            currency.Id.Returns(CURRENCY_ID);
            return currency;
        }
    }
}