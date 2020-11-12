using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StickyTeam.FashionClash.Customization.Core
{
    public interface Item
    {
        Sprite Sprite { get; }
        bool CanChangeColor { get; }
        bool IsPurchased { get; }
        bool IsUnlocked { get; }
    }
}
