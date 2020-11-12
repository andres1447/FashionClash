using Assets.Scripts.StickyTeam.FashionClash.Customization.Core;
using UnityEngine;
using UnityEngine.UI;

namespace StickyTeam.FashionClash.Customization.Infrastructure
{
    public class UnitySelectableCategoryWidget : UnitySelectableWidget<Category>
    {
        [SerializeField] private Image _thumb;
        
        protected override void Map(Category data)
        {
            _thumb.sprite = data.Sprite;
        }
    }
}