using System;
using System.Collections.Generic;

namespace Assets.Scripts.StickyTeam.FashionClash.Customization.Core
{
    public interface CustomizationView
    {
        IObservable<string> CategorySelected { get; }
        IObservable<string> ItemSelected { get; }
        IObservable<string> ColorSelected { get; }

        void DisplayItems(List<Item> items);
        void EquipItem(Item selectedItem);
    }
}
