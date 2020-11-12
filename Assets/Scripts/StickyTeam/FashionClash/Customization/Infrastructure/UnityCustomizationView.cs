using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.StickyTeam.FashionClash.Customization.Core;
using UniRx;
using UnityEngine;

namespace StickyTeam.FashionClash.Customization.Infrastructure
{
    public class UnityCustomizationView : MonoBehaviour, CustomizationView
    {
        private const int MAX_CATEGORIES = 10;
        private const int MAX_ITEMS = 30;
        
        [SerializeField] private Transform _categoriesContainer;
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private UnitySelectableCategoryWidget _categoryPrefab;
        [SerializeField] private UnitySelectableItemWidget _itemPrefab;
        [SerializeField] private GameObject _colorBar;
        [SerializeField] private UnityItemPurchaseConfirmationModal _purchaseConfirmationModal;
        
        Subject<Unit> OnEnabledSubject = new Subject<Unit>();
        public IObservable<Unit> OnEnabled => OnEnabledSubject.AsObservable();
        
        public IObservable<Category> CategorySelected { get; private set; }
        public IObservable<Item> ItemSelected { get; private set; }
        public IObservable<Color> ColorSelected { get; }
        
        public IObservable<Unit> OnComplete { get; }

        private List<UnitySelectableCategoryWidget> _categories = new List<UnitySelectableCategoryWidget>(MAX_CATEGORIES);
        private List<UnitySelectableItemWidget> _items = new List<UnitySelectableItemWidget>(MAX_ITEMS);

        protected void Awake()
        {
            for (var i = 0; i < MAX_CATEGORIES; ++i)
                Instantiate(_categoryPrefab, _categoriesContainer);
            
            for (var i = 0; i < MAX_ITEMS; ++i)
                Instantiate(_itemPrefab, _itemsContainer);
            
            CategorySelected = _categories.Select(widget => widget.OnSelected).Merge();
            ItemSelected = _items.Select(widget => widget.OnSelected).Merge();
        }

        protected virtual void OnEnable()
        {
            OnEnabledSubject.OnNext(Unit.Default);
        }
        
        public void DisplayCategories(List<Category> categories)
        {
            _categories.MapMany(categories);
        }

        public void DisplayItems(List<Item> items)
        {
            _items.MapMany(items);
        }

        public void EquipItem(Item selectedItem)
        {
        }

        public void HideColorBar()
        {
            _colorBar.SetActive(false);
        }

        public void DisplayColorBar()
        {
            _colorBar.SetActive(true);
        }

        public IObservable<bool> ShowPurchaseConfirmation(Item item)
        {
            return _purchaseConfirmationModal.Show(item);
        }

        public void ShowItemLocked(Item item)
        {
            
        }
    }
}