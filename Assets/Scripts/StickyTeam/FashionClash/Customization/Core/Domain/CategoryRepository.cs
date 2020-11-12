using System;
using System.Collections.Generic;

namespace StickyTeam.FashionClash.Customization.Core.Domain
{
    public interface CategoryRepository
    {
        IObservable<List<Category>> GetAll();
    }
}
