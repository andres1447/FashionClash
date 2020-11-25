using System.Collections.Generic;
using System.Linq;
using StickyTeam.FashionClash.Shared.Core;
using StickyTeam.FashionClash.Shared.Infrastructure;
using StickyTeam.FashionClash.Wallet.Core.Service;
using StickyTeam.FashionClash.Wallet.Infrastructure;
using StickyTeam.Infrastructure.Container;
using UnityEngine;

namespace StickyTeam.FashionClash.Wallet
{
    [CreateAssetMenu(menuName = "Fashion Clash/Wallet/Module")]
    public class WalletModule : UnityModule
    {
        [SerializeField] private List<UnityCurrency> _categories;
        public override void Register(Resolver resolver)
        {
            var currencies = _categories.Cast<Currency>().ToList();
            
            resolver.Register(() =>
                new WalletService(new WalletRepositoryAdapter(currencies, resolver.Resolve<PlayerDataRepository>()))
            );
        }
    }
}