using StickyTeam.FashionClash.Shared.Core;
using UnityEngine;

namespace StickyTeam.FashionClash.Shared.Infrastructure
{
    [CreateAssetMenu(menuName = "Fashion Clash/Currency")]
    public class UnityCurrency : ScriptableObject, Currency
    {
        [SerializeField] private string _id;
        public string Id => _id;

        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;
    }
}