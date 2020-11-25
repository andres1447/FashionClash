using StickyTeam.FashionClash.Customization.Core.Domain;
using StickyTeam.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace StickyTeam.FashionClash.Customization.Infrastructure.Presentation
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