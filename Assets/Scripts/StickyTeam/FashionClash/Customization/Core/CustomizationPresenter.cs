using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Assets.Scripts.StickyTeam.FashionClash.Customization.Core
{
    public class CustomizationPresenter
    {
        CustomizationView _view;
        CategoryRepository _categoryRepository;
        NavigatorGateway _navigator;

        public CustomizationPresenter(CustomizationView view, NavigatorGateway navigator, CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _view = view;
            _navigator = navigator;

            _view.OnEnabled.Subscribe(DisplayCategories);
            _view.CategorySelected.Subscribe(OnCategorySelected);
            
            _view.ItemSelected
                .Where(IsItemUnlocked)
                .SelectMany(AskPurchaseIfNotPurchased)
                .Where(item => item.IsPurchased && item.IsUnlocked)
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
            return _view.ShowPurchaseConfirmation(item)
                .Where(res => true)
                .Select(_ => PurchaseItem(item));
        }

        private Item PurchaseItem(Item item)
        {
            return item;
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
