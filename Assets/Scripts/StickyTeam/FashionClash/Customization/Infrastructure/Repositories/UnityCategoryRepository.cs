using System;
using System.Collections.Generic;
using System.Linq;
using StickyTeam.FashionClash.Customization.Core.Domain;
using UniRx;
using UnityEngine;

namespace StickyTeam.FashionClash.Customization.Infrastructure.Repositories
{
    public class InMemoryCategoryRepository : CategoryRepository
    {
        private List<Category> _categories;

        public InMemoryCategoryRepository(List<Category> categories)
        {
            _categories = categories;
        }

        public IObservable<List<Category>> GetAll()
        {
            return Observable.Return(_categories).Take(1);
        }
    }
}