using System.Collections.Generic;
using Assets.Scripts.StickyTeam.FashionClash.Customization.Core;
using UnityEngine;

namespace StickyTeam.FashionClash.Customization.Core.Domain
{
    public interface Category
    {
        string Id { get; }
        Sprite Sprite { get; }
        List<Item> Items { get; }
    }
}
