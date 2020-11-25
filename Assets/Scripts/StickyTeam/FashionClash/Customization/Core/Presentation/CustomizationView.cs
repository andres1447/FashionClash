using System;
using System.Collections.Generic;
using StickyTeam.FashionClash.Customization.Core.Domain;
using UniRx;
using UnityEngine;

namespace StickyTeam.FashionClash.Customization.Core.Presentation
{
    public interface CustomizationView
    {
        IObservable<Unit> OnEnabled { get; }
        IObservable<Category> CategorySelected { get; }
        IObservable<Item> ItemSelected { get; }
        IObservable<Color> ColorSelected { get; }
        IObservable<Unit> OnComplete { get; }

        void DisplayCategories(List<Category> categories);
        void DisplayItems(List<Item> items);
        void EquipItem(IEnumerable<ItemPart> item);
        void HideColorBar();
        void DisplayColorBar();
        IObservable<bool> ShowPurchaseConfirmation(Item item);
        void ShowItemLocked(ItemDetail item);
    }
}
