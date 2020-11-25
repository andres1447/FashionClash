using StickyTeam.FashionClash.Customization.Core.Domain;
using StickyTeam.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace StickyTeam.FashionClash.Customization.Infrastructure.Presentation
{
    public class UnitySelectableItemWidget : UnitySelectableWidget<Item>
    {
        [SerializeField] private Image _thumb;
        
        protected override void Map(Item data)
        {
            _thumb.sprite = data.ItemDetail.Sprite;
        }
    }
}