using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Assets.Scripts.StickyTeam.FashionClash.Customization.Core;
using NSubstitute;
using NUnit.Framework;
using UniRx;
using UnityEngine.TestTools;

namespace StickyTeam.FashionClash.Customization.Tests
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

        [SetUp]
        public void SetUp()
        {
            _view = Substitute.For<CustomizationView>();
            _view.CategorySelected.Returns(_categorySelected.AsObservable());
            _view.ItemSelected.Returns(_itemSelected.AsObservable());
            _view.OnComplete.Returns(_complete.AsObservable());
            _view.OnEnabled.Returns(_enabled.AsObservable());
            
            _navigator = Substitute.For<NavigatorGateway>();
            _categoryRepository = Substitute.For<CategoryRepository>();
            _presenter = new CustomizationPresenter(_view, _navigator, _categoryRepository);
        }
        
        [Test]
        public void display_items_on_category_selected()
        {
            _categorySelected.OnNext(Substitute.For<Category>());
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

        private void WhenSelectedItem(Item item)
        {
            _itemSelected.OnNext(item);
        }

    }
    public static partial class Given
    {
        public static Item AnItem()
        {
            return Substitute.For<Item>();
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
    }
}
