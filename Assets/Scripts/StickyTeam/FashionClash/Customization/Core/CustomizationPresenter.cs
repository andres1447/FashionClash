using System;
using Assets.Scripts.StickyTeam.FashionClash.Customization.Core;
using StickyTeam.FashionClash.Customization.Core.Actions;
using StickyTeam.FashionClash.Customization.Core.Domain;
using UniRx;

namespace StickyTeam.FashionClash.Customization.Core
{
    public class CustomizationPresenter
    {
        private readonly CustomizationView _view;
        private readonly CategoryRepository _categoryRepository;
        private readonly NavigatorGateway _navigator;
        private readonly PurchaseItem _purchaseItem;

        public CustomizationPresenter(CustomizationView view, NavigatorGateway navigator, CategoryRepository categoryRepository, PurchaseItem purchaseItem)
        {
            _categoryRepository = categoryRepository;
            _purchaseItem = purchaseItem;
            _view = view;
            _navigator = navigator;

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
                _view.ShowItemLocked(item);

            return item.IsUnlocked;
        }

        private void DisplayCategories(Unit unit)
        {
            _categoryRepository.GetAll().First().Subscribe(_view.DisplayCategories);
        }

        private void OnCategorySelected(Category category)
        {
            _view.DisplayItems(category.Items);
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
                .Execute(item)
                .Where(success => success)
                .Select(_ => item);
        }

        private void OnSelectItem(Item item)
        {
            if (item.CanChangeColor)
                _view.DisplayColorBar();
            else
                _view.HideColorBar();
            
            _view.EquipItem(item);
        }

        private void GoToNextStep(Unit unit)
        {
            _navigator.GoToNextStep();
        }
    }
}
