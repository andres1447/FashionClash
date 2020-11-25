using NSubstitute;
using NUnit.Framework;
using StickyTeam.FashionClash.Shared.Core;
using StickyTeam.FashionClash.Wallet.Core.Domain;
using StickyTeam.FashionClash.Wallet.Core.Service;
using UniRx;

namespace StickyTeam.FashionClash.Wallet.Test
{
    public class WalletServiceShould
    {
        private readonly Currency CURRENCY = Substitute.For<Currency>();
        private const int AMOUNT = 10;
        
        private PlayerWallet _wallet;
        private WalletRepository _repository;
        private WalletService _walletService;

        [SetUp]
        public void SetUp()
        {
            _wallet = new PlayerWallet();
            
            _repository = Substitute.For<WalletRepository>();
            _repository.Get().Returns(Observable.Return(_wallet));
            
            _walletService = new WalletService(_repository);
        }

        [Test]
        public void save_on_add()
        {
            WhenAddToWallet(CURRENCY, AMOUNT);
            
            ThenRepositoryShouldSave();
        }

        private void WhenAddToWallet(Currency currency, int amount)
        {
            _walletService.Add(currency, amount).Subscribe();
        }

        private void ThenRepositoryShouldSave()
        {
            _repository.Received(1).Save();
        }

        [Test]
        public void save_on_take()
        {
            GivenWalletWith(CURRENCY, AMOUNT);
            
            WhenTakeFromWallet(CURRENCY, AMOUNT);
            
            ThenRepositoryShouldSave();
        }

        private void GivenWalletWith(Currency currency, int amount)
        {
            _wallet.Add(currency, amount);
        }

        private void WhenTakeFromWallet(Currency currency, int amount)
        {
            _walletService.Take(currency, amount).Subscribe();
        }

        [Test]
        public void dont_save_if_cant_take()
        {
            WhenTakeFromWallet(CURRENCY, AMOUNT);

            ThenRepositoryShouldNotSave();
        }

        private void ThenRepositoryShouldNotSave()
        {
            _repository.Received(0).Save();
        }
    }
}