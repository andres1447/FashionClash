using StickyTeam.FashionClash.Customization.Core.Domain;
using StickyTeam.FashionClash.Customization.Core.Gateways;
using StickyTeam.FashionClash.Shared.Core;
using UniRx;

namespace StickyTeam.FashionClash.Customization.Infrastructure.Gateways
{
    public class PlayerProgressGatewayAdapter : PlayerProgressGateway
    {
        private readonly PlayerDataRepository _playerDataRepository;

        public PlayerProgressGatewayAdapter(PlayerDataRepository playerDataRepository)
        {
            _playerDataRepository = playerDataRepository;
        }

        public int GetLevel()
        {
            return _playerDataRepository.GetLevel().Wait();
        }
    }
}