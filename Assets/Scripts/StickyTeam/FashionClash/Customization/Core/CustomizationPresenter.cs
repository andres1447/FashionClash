using System;
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

            _view.OnEnable.Subscribe(DisplayCategories);
            _view.CategorySelected.Subscribe(DisplayItems);
            _view.ItemSelected.Subscribe(EquipItem);
            _view.OnComplete.Subscribe(_ => _navigator.GoToNextStep());
        }

        public void DisplayCategories(Unit unit)
        {
            _categoryRepository.GetAll().First().Subscribe(_view.DisplayCategories);
        }

        public void DisplayItems(Category category)
        {
            _view.DisplayItems(category.Items);
        }

        private void EquipItem(Item item)
        {
            _view.EquipItem(item);
        }

    }
}
