using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StickyTeam.FashionClash.Customization.Core
{
    public interface Category
    {
        string Id { get; }
        Sprite Sprite { get; }
        List<Item> Items { get; }
    }
}
