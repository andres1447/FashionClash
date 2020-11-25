using StickyTeam.FashionClash.Shared.Core;
using StickyTeam.FashionClash.Shared.Infrastructure.SavedData;
using StickyTeam.Infrastructure.Container;
using UnityEngine;

namespace StickyTeam.FashionClash.Shared.Infrastructure
{
    [CreateAssetMenu(menuName = "Fashion Clash/Shared/Module")]
    public class SharedModule : UnityModule
    {
        public override void Register(Resolver resolver)
        {
            resolver.Register<PlayerDataRepository>(new InMemoryPlayerDataRepository());
        }
    }
}