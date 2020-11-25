using System.Collections.Generic;

namespace StickyTeam.Infrastructure
{
    public static class UnitySelectableWidgetListExtensions
    {
        public static void MapMany<T, K>(this List<T> list, List<K> items) where T : UnitySelectableWidget<K>
        {
            var itemsCount = items.Count;
            for (var i = 0; i < itemsCount; ++i)
            {
                list[i].Display(items[i]);
            }

            var elemsCount = list.Count;
            for (var i = itemsCount; i < elemsCount; ++i)
            {
                list[i].Hide();
            }
        }
    }
}