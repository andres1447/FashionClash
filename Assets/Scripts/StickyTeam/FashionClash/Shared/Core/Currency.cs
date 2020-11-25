using UnityEngine;

namespace StickyTeam.FashionClash.Shared.Core
{
    public interface Currency
    {
        string Id { get; }
        Sprite Sprite { get; }
    }
}