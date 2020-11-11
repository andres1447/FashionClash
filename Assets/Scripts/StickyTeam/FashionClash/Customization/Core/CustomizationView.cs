﻿using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.StickyTeam.FashionClash.Customization.Core
{
    public interface CustomizationView
    {
        IObservable<Unit> OnEnable { get; }
        IObservable<Category> CategorySelected { get; }
        IObservable<Item> ItemSelected { get; }
        IObservable<Color> ColorSelected { get; }
        IObservable<Unit> OnComplete { get; }

        void DisplayCategories(List<Category> categories);
        void DisplayItems(List<Item> items);
        void EquipItem(Item selectedItem);
    }
}
