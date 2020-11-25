using System;
using StickyTeam.FashionClash.Customization.Core.Actions;
using StickyTeam.FashionClash.Customization.Core.Domain;
using StickyTeam.FashionClash.Customization.Core.Gateways;
using UniRx;

namespace StickyTeam.FashionClash.Customization.Core.Presentation
{
    public class CustomizationPresenter
    {
        private readonly CustomizationView _view;
        private readonly GetItems _getItems;
        private readonly PurchaseItem _purchaseItem;
        private readonly CategoryRepository _categoryRepository;
        private readonly NavigatorGateway _navigator;

        private Category _selectedCategory;

        public CustomizationPresenter(
            CustomizationView view,
            NavigatorGateway navigator,
            CategoryRepository categoryRepository,
            GetItems getItems,
            PurchaseItem purchaseItem)
        {
            _view = view;
            _navigator = navigator;
            _categoryRepository = categoryRepository;
            _getItems = getItems;
            _purchaseItem = purchaseItem;

            _view.OnEnabled.Subscribe(DisplayCategories);
            _view.CategorySelected.Subscribe(OnCategorySelected);
            
            _view.ItemSelected
                .Where(IsItemUnlocked)
                .SelectMany(AskPurchaseIfNotPurchased)
                .Do(OnSelectItem)
                .Subscribe();
            
            _view.OnComplete.Subscribe(GoToNextStep);
        }

        private bool IsItemUnlocked(Item item)
        {
            if (!item.IsUnlocked)
                _view.ShowItemLocked(item.ItemDetail);

            return item.IsUnlocked;
        }

        private void DisplayCategories(Unit unit)
        {
            _categoryRepository.GetAll().Subscribe(_view.DisplayCategories);
        }

        private void OnCategorySelected(Category category)
        {
            _selectedCategory = category;
            DisplaySelectedCategoryItems();
        }

        private void DisplaySelectedCategoryItems()
        {
            _getItems.Execute(_selectedCategory)
                            .Subscribe(_view.DisplayItems);
        }

        private IObservable<Item> AskPurchaseIfNotPurchased(Item item)
        {
            return item.IsPurchased
                ? Observable.Return(item)
                : _view.ShowPurchaseConfirmation(item)
                    .SelectMany(success => success ? PurchaseItem(item) : Observable.Empty<Item>());
        }

        private IObservable<Item> PurchaseItem(Item item)
        {
            return _purchaseItem
                .Execute(item.ItemDetail)
                .Where(success => success)
                .Select(_ => item)
                .Do(_ => DisplaySelectedCategoryItems());
        }

        private void OnSelectItem(Item item)
        {
            if (item.CanChangeColor)
                _view.DisplayColorBar();
            else
                _view.HideColorBar();
            
            _view.EquipItem(item.ItemDetail.Parts);
        }

        private void GoToNextStep(Unit unit)
        {
            _navigator.GoToNextStep();
        }
    }
}
