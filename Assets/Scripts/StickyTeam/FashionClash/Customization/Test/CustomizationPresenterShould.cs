using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using StickyTeam.FashionClash.Customization.Core;
using StickyTeam.FashionClash.Customization.Core.Actions;
using StickyTeam.FashionClash.Customization.Core.Domain;
using StickyTeam.FashionClash.Customization.Core.Gateways;
using StickyTeam.FashionClash.Customization.Core.Presentation;
using UniRx;

namespace StickyTeam.FashionClash.Customization.Test
{
    public class CustomizationPresenterShould
    {
        private CustomizationView _view;
        private NavigatorGateway _navigator;
        private CategoryRepository _categoryRepository;

        private Subject<Category> _categorySelected = new Subject<Category>();
        private Subject<Item> _itemSelected = new Subject<Item>();
        private Subject<Unit> _complete = new Subject<Unit>();
        private Subject<Unit> _enabled = new Subject<Unit>();
        private Subject<bool> _confirmPurchase = new Subject<bool>();
        
        private PurchaseItem _purchaseItem;
        private GetItems _getItems;

        [SetUp]
        public void SetUp()
        {
            _navigator = Substitute.For<NavigatorGateway>();
            _categoryRepository = Substitute.For<CategoryRepository>();
            SetUpPurchaseItemAction();
            SetUpView();
            SetUpGetItemsAction();
            _ = new CustomizationPresenter(_view, _navigator, _categoryRepository, _getItems, _purchaseItem);
        }

        private void SetUpGetItemsAction()
        {
            _getItems = Substitute.For<GetItems>();
            _getItems.Execute(Arg.Any<Category>()).Returns(Observable.Return(new List<Item>()));
        }

        private void SetUpPurchaseItemAction()
        {
            _purchaseItem = Substitute.For<PurchaseItem>();
            _purchaseItem.Execute(Arg.Any<ItemDetail>())
                .Returns(Observable.Return(true));
        }

        private void SetUpView()
        {
            _view = Substitute.For<CustomizationView>();
            _view.CategorySelected.Returns(_categorySelected.AsObservable());
            _view.ItemSelected.Returns(_itemSelected.AsObservable());
            _view.OnComplete.Returns(_complete.AsObservable());
            _view.OnEnabled.Returns(_enabled.AsObservable());
            _view.ShowPurchaseConfirmation(Arg.Any<Item>()).Returns(_confirmPurchase.AsObservable());
        }

        [Test]
        public void display_items_on_category_selected()
        {
            var category = Given.AnCategory();
            _categorySelected.OnNext(category);
            _view.Received(1).DisplayItems(Arg.Any<List<Item>>());
        }

        [Test]
        public void show_locked_item_if_locked_item_selected()
        {
            var item = Given.AnItem().Locked();
            WhenSelectedItem(item);
            _view.Received(1).ShowItemLocked(item.ItemDetail);
        }

        [Test]
        public void show_purchase_confirm_if_not_purchased_item_selected()
        {
            var item = Given.AnItem().Unlocked().NotPurchased();
            WhenSelectedItem(item);
            _view.Received(1).ShowPurchaseConfirmation(item);
        }

        [Test]
        public void equip_item_if_purchase_confirmed()
        {
            var item = Given.AnItem().Unlocked().NotPurchased();
            WhenSelectedItem(item);
            WhenPurchaseConfirmed(true);
            ThenEquipItem();
        }

        [Test]
        public void dont_equip_item_if_puchase_cancelled()
        {
            var item = Given.AnItem().Unlocked().NotPurchased();
            WhenSelectedItem(item);
            WhenPurchaseConfirmed(false);
            ThenDontEquipItem();
        }

        private void WhenPurchaseConfirmed(bool value)
        {
            _confirmPurchase.OnNext(value);
        }

        [Test]
        public void equip_item_if_unlocked_and_purchased()
        {
            var item = Given.AnItem().Unlocked().Purchased();
            WhenSelectedItem(item);
            ThenEquipItem();
        }

        private void ThenEquipItem()
        {
            _view.Received(1).EquipItem(Arg.Any<IEnumerable<ItemPart>>());
        }

        private void ThenDontEquipItem()
        {
            _view.Received(0).EquipItem(Arg.Any<IEnumerable<ItemPart>>());
        }

        [Test]
        public void show_color_bar_if_selected_item_with_color()
        {
            var item = Given.AnItem().Unlocked().Purchased().CanChangeColor();
            WhenSelectedItem(item);
            _view.Received(1).DisplayColorBar();
        }

        private void WhenSelectedItem(Item item)
        {
            _itemSelected.OnNext(item);
        }
    }
    
    public static class Given
    {
        public static Item AnItem()
        {
            var item = Substitute.For<Item>();
            var detail = Substitute.For<ItemDetail>();
            item.ItemDetail.Returns(detail);
            return item;
        }

        public static Category AnCategory()
        {
            return Substitute.For<Category>();
        }

        public static Item Locked(this Item item)
        {
            item.IsUnlocked.Returns(false);
            return item;
        }

        public static Item Unlocked(this Item item)
        {
            item.IsUnlocked.Returns(true);
            return item;
        }

        public static Item Purchased(this Item item)
        {
            item.IsPurchased.Returns(true);
            return item;
        }

        public static Item NotPurchased(this Item item)
        {
            item.IsPurchased.Returns(false);
            return item;
        }

        public static Item CanChangeColor(this Item item)
        {
            item.CanChangeColor.Returns(true);
            return item;
        }

        public static Item CannotChangeColor(this Item item)
        {
            item.CanChangeColor.Returns(false);
            return item;
        }
    }
}
