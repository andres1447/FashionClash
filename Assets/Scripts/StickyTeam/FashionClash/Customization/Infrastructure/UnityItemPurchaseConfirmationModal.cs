using Assets.Scripts.StickyTeam.FashionClash.Customization.Core;
using StickyTeam.FashionClash.Customization.Core.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace StickyTeam.FashionClash.Customization.Infrastructure
{
    public class UnityItemPurchaseConfirmationModal : UnityConfirmModal<Item>
    {
        [SerializeField] private Image _thumb;
        [SerializeField] private Text _price;
        
        protected override void Map(Item data)
        {
            _thumb.sprite = data.Sprite;
        }
    }
}