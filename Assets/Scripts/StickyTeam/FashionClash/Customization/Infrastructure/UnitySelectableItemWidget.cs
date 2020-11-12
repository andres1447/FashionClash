using Assets.Scripts.StickyTeam.FashionClash.Customization.Core;
using UnityEngine;
using UnityEngine.UI;

namespace StickyTeam.FashionClash.Customization.Infrastructure
{
    public class UnitySelectableItemWidget : UnitySelectableWidget<Item>
    {
        [SerializeField] private Image _thumb;
        
        protected override void Map(Item data)
        {
            _thumb.sprite = data.Sprite;
        }
    }
}