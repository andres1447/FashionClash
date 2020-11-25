using System.Collections.Generic;
using System.Linq;
using StickyTeam.FashionClash.Customization.Core.Domain;
using UnityEngine;

namespace StickyTeam.FashionClash.Customization.Infrastructure
{
    [CreateAssetMenu(menuName = "Fashion Clash/Customization/Category")]
    public class UnityCategory : ScriptableObject, Category
    {
        [SerializeField] private string _id;
        public string Id => _id;
        
        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField] public List<UnityItem> items;
        public List<ItemDetail> Items { get; private set; }
        
        public void OnEnable()
        {
            Items = items.Cast<ItemDetail>().ToList();
        }
    }
}