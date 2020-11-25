using StickyTeam.FashionClash.Customization.Core.Domain;
using StickyTeam.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace StickyTeam.FashionClash.Customization.Infrastructure.Presentation
{
    public class UnityItemPurchaseConfirmationModal : UnityConfirmModal<Item>
    {
        [SerializeField] private Image _thumb;
        [SerializeField] private Text _price;
        
        protected override void Map(Item data)
        {
            _thumb.sprite = data.ItemDetail.Sprite;
            _price.text = data.ItemDetail.Price.Amount.ToString();
            Confirm.enabled = data.CanPurchase;
        }
    }
}