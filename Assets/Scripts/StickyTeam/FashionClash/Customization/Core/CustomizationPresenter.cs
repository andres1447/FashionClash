using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Assets.Scripts.StickyTeam.FashionClash.Customization.Core
{
    public class CustomizationPresenter
    {
        CustomizationView _view;
        List<Category> _categories;

        Category _selectedCategory;
        Item _selectedItem;

        public CustomizationPresenter(CustomizationView view, List<Category> categories)
        {
            _view = view;
            _categories = categories;

            _view.CategorySelected.Subscribe(DisplayItems);
            _view.ItemSelected.Subscribe(EquipItem);
        }

        public void DisplayItems(string id)
        {
            _selectedCategory = _categories.First(x => x.Id == id);
            _view.DisplayItems(_selectedCategory.Items);
        }

        private void EquipItem(string id)
        {
            _selectedItem = _selectedCategory.Items.First(x => x.Id == id);
            _view.EquipItem(_selectedItem);
        }

    }
}
