namespace StickyTeam.FashionClash.Customization.Core.Domain
{
    public class Item
    {
        public virtual ItemDetail ItemDetail { get; }
        public virtual bool IsPurchased { get; set; }
        public virtual bool CanPurchase { get; set; }
        public virtual bool IsUnlocked { get; set; }
        public virtual bool CanChangeColor => ItemDetail.CanChangeColor;

        public Item()
        {
        }

        public Item(ItemDetail itemDetail)
        {
            ItemDetail = itemDetail;
        }
    }
}