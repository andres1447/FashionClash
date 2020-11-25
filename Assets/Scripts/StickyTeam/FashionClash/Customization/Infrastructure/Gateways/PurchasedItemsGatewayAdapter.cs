using System;
using StickyTeam.FashionClash.Customization.Core.Domain;
using StickyTeam.FashionClash.Customization.Core.Gateways;
using StickyTeam.FashionClash.Shared.Core;
using UniRx;

namespace StickyTeam.FashionClash.Customization.Infrastructure.Gateways
{
    public class PurchasedItemsGatewayAdapter : PurchasedItemsGateway
    {
        private PlayerDataRepository _playerDataRepository;

        public PurchasedItemsGatewayAdapter(PlayerDataRepository playerDataRepository)
        {
            _playerDataRepository = playerDataRepository;
        }
        
        public IObservable<Unit> Add(ItemDetail item)
        {
            return _playerDataRepository.AddPurchasedItem(item.Id);
        }

        public bool IsPurchased(ItemDetail item)
        {
            return _playerDataRepository.GetPurchasedItems().Wait().Contains(item.Id);
        }
    }
}