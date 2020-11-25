using System;
using System.Collections.Generic;
using UnityEngine;

namespace StickyTeam.FashionClash.Customization.Core.Domain
{
    public interface ItemDetail
    {
        string Id { get; }
        Sprite Sprite { get; }
        int RequiredLevel { get; }
        Category Category { get; }
        Price Price { get; }
        bool CanChangeColor { get; }
        ItemPart[] Parts { get; }
    }

    public interface ItemPart
    {
        string Category { get; }
        string Label { get; }
    }
}
