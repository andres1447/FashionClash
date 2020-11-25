using System.Collections.Generic;
using System.Linq;
using StickyTeam.FashionClash.Customization.Core.Actions;
using StickyTeam.FashionClash.Customization.Core.Domain;
using StickyTeam.FashionClash.Customization.Core.Gateways;
using StickyTeam.FashionClash.Customization.Infrastructure.Gateways;
using StickyTeam.FashionClash.Customization.Infrastructure.Repositories;
using StickyTeam.FashionClash.Shared.Core;
using StickyTeam.FashionClash.Wallet.Core.Service;
using StickyTeam.Infrastructure.Container;
using UnityEngine;

namespace StickyTeam.FashionClash.Customization.Infrastructure
{
    [CreateAssetMenu(menuName = "Fashion Clash/Customization/Module")]
    public class CustomizationModule : UnityModule
    {
        [SerializeField] private List<UnityCategory> _categories;

        public override void Register(Resolver resolver)
        {
            var categories = _categories.Cast<Category>().ToList();
            resolver.Register<CategoryRepository>(new InMemoryCategoryRepository(categories));
            
            resolver.Register<NavigatorGateway>(new NavigatorGatewayAdapter());
            
            resolver.Register<WalletGateway>(() => 
                new WalletGatewayAdapter(resolver.Resolve<WalletService>())
            );
            
            resolver.Register<PurchasedItemsGateway>(() =>
                new PurchasedItemsGatewayAdapter(resolver.Resolve<PlayerDataRepository>())
            );
            
            resolver.Register<PlayerProgressGateway>(() =>
                new PlayerProgressGatewayAdapter(resolver.Resolve<PlayerDataRepository>())
            );
            
            resolver.Register(() =>
                new GetItems(
                    resolver.Resolve<PlayerProgressGateway>(),
                    resolver.Resolve<PurchasedItemsGateway>(),
                    resolver.Resolve<WalletGateway>())
            );
            
            resolver.Register(() =>
                new PurchaseItem(resolver.Resolve<WalletGateway>(), 
                    resolver.Resolve<PurchasedItemsGateway>())
            );
        }
    }
}