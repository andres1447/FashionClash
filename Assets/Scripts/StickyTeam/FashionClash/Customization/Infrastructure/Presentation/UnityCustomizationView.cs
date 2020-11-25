using System;
using System.Collections.Generic;
using System.Linq;
using StickyTeam.FashionClash.Customization.Core.Actions;
using StickyTeam.FashionClash.Customization.Core.Domain;
using StickyTeam.FashionClash.Customization.Core.Gateways;
using StickyTeam.FashionClash.Customization.Core.Presentation;
using StickyTeam.FashionClash.Customization.Infrastructure.Presentation.Character;
using StickyTeam.Infrastructure;
using StickyTeam.Infrastructure.Container;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace StickyTeam.FashionClash.Customization.Infrastructure.Presentation
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
        [SerializeField] private Button _completeButton;
        [SerializeField] private UnityCharacterModel _character;
        
        Subject<Unit> OnEnabledSubject = new Subject<Unit>();
        public IObservable<Unit> OnEnabled => OnEnabledSubject.AsObservable();
        
        public IObservable<Category> CategorySelected { get; private set; }
        public IObservable<Item> ItemSelected { get; private set; }
        public IObservable<Color> ColorSelected { get; }
        
        public IObservable<Unit> OnComplete { get; private set; }

        private List<UnitySelectableCategoryWidget> _categories = new List<UnitySelectableCategoryWidget>(MAX_CATEGORIES);
        private List<UnitySelectableItemWidget> _items = new List<UnitySelectableItemWidget>(MAX_ITEMS);

        protected void Awake()
        {
            _character = FindObjectOfType<UnityCharacterModel>();
            
            for (var i = 0; i < MAX_CATEGORIES; ++i)
                _categories.Add(Instantiate(_categoryPrefab, _categoriesContainer));
            
            for (var i = 0; i < MAX_ITEMS; ++i)
                _items.Add(Instantiate(_itemPrefab, _itemsContainer));
            
            CategorySelected = _categories.Select(widget => widget.OnSelected).Merge();
            ItemSelected = _items.Select(widget => widget.OnSelected).Merge();
            OnComplete = _completeButton.OnClickAsObservable();
            
            _ = new CustomizationPresenter(this,
                UnityContainer.Resolver.Resolve<NavigatorGateway>(),
                UnityContainer.Resolver.Resolve<CategoryRepository>(),
                UnityContainer.Resolver.Resolve<GetItems>(),
                UnityContainer.Resolver.Resolve<PurchaseItem>()
            );
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

        public void EquipItem(IEnumerable<ItemPart> parts)
        {
            _character.EquipItem(parts);
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

        public void ShowItemLocked(ItemDetail item)
        {
            
        }
    }
}