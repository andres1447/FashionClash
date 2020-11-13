using UnityEngine;

namespace StickyTeam.FashionClash.Customization.Core.Domain
{
    public interface Item
    {
        Sprite Sprite { get; }
        bool CanChangeColor { get; }
        bool IsPurchased { get; }
        bool IsUnlocked { get; }
        Price Price { get; }

        void Purchase();
    }
}
