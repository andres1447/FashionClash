using System;
using StickyTeam.FashionClash.Customization.Core.Domain;
using StickyTeam.FashionClash.Shared.Core;
using StickyTeam.FashionClash.Shared.Infrastructure;
using UnityEngine;

namespace StickyTeam.FashionClash.Customization.Infrastructure
{
    [CreateAssetMenu(menuName = "Fashion Clash/Customization/Item")]
    public class UnityItem : ScriptableObject, ItemDetail
    {
        [SerializeField] private string _id;
        public string Id => _id;
        
        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField] private int _requiredLevel;
        public int RequiredLevel => _requiredLevel;

        [SerializeField] public UnityCategory category;
        public Category Category => category;
        
        [SerializeField] private ItemPrice _price;
        public Price Price => _price;

        [SerializeField] private bool _canChangeColor;
        public bool CanChangeColor => _canChangeColor;

        [SerializeField] private UnityItemPart[] _parts;
        public ItemPart[] Parts => _parts;
    }

    [Serializable]
    public class ItemPrice : Price
    {
        [SerializeField] private UnityCurrency _currency;
        public Currency Currency => _currency;

        [SerializeField] private int _amount;
        public int Amount => _amount;
    }

    [Serializable]
    public class UnityItemPart : ItemPart
    {
        [SerializeField] private string _category;
        public string Category => _category;

        [SerializeField] private string _label;
        public string Label => _label;
    }
}