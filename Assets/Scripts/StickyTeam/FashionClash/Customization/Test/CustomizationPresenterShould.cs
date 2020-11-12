using System;
using System.Collections.Generic;
using Assets.Scripts.StickyTeam.FashionClash.Customization.Core;
using NSubstitute;
using NUnit.Framework;
using StickyTeam.FashionClash.Customization.Core;
using StickyTeam.FashionClash.Customization.Core.Actions;
using StickyTeam.FashionClash.Customization.Core.Domain;
using UniRx;

namespace StickyTeam.FashionClash.Customization.Test
{
    public class CustomizationPresenterShould
    {
        private CustomizationView _view;
        private CustomizationPresenter _presenter;
        private NavigatorGateway _navigator;
        private CategoryRepository _categoryRepository;

        private Subject<Category> _categorySelected = new Subject<Category>();
        private Subject<Item> _itemSelected = new Subject<Item>();
        private Subject<Unit> _complete = new Subject<Unit>();
        private Subject<Unit> _enabled = new Subject<Unit>();
        private Subject<bool> _confirmPurchase = new Subject<bool>();
        private PurchaseItem _purchaseItem;

        [SetUp]
        public void SetUp()
        {
            SetUpView();
            _navigator = Substitute.For<NavigatorGateway>();
            _categoryRepository = Substitute.For<CategoryRepository>();
            SetUpPurchaseAction();
            _presenter = new CustomizationPresenter(_view, _navigator, _categoryRepository, _purchaseItem);
        }

        private void SetUpPurchaseAction()
        {
            _purchaseItem = Substitute.For<PurchaseItem>();
            _purchaseItem.Execute(Arg.Any<Item>())
                         .Returns(Observable.Return(true))
                         .AndDoes(info => info.Arg<Item>().Purchase());
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
            _view.Received(1).ShowItemLocked(item);
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
            ThenEquipItem(item);
        }

        [Test]
        public void dont_equip_item_if_puchase_cancelled()
        {
            var item = Given.AnItem().Unlocked().NotPurchased();
            WhenSelectedItem(item);
            WhenPurchaseConfirmed(false);
            ThenDontEquipItem(item);
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
            ThenEquipItem(item);
        }

        private void ThenEquipItem(Item item)
        {
            _view.Received(1).EquipItem(item);
        }

        private void ThenDontEquipItem(Item item)
        {
            _view.Received(0).EquipItem(item);
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
            item.When(x => x.Purchase()).Do(_ => item.IsPurchased.Returns(true));
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
