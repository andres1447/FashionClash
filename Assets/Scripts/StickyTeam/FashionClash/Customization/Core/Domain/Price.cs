using StickyTeam.FashionClash.Shared.Core;

namespace StickyTeam.FashionClash.Customization.Core.Domain
{
    public interface Price
    {
        Currency Currency { get; }
        int Amount { get; }
    }
}