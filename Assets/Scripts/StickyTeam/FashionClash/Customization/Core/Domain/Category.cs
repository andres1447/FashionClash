using System.Collections.Generic;
using UnityEngine;

namespace StickyTeam.FashionClash.Customization.Core.Domain
{
    public interface Category
    {
        string Id { get; }
        Sprite Sprite { get; }
        List<ItemDetail> Items { get; }
    }
}
