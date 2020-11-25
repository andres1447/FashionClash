using System.Collections.Generic;
using System.Linq;
using StickyTeam.FashionClash.Customization.Core.Domain;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

namespace StickyTeam.FashionClash.Customization.Infrastructure.Presentation.Character
{
    public class UnityCharacterModel : MonoBehaviour
    {
        private Dictionary<string, ModelPart> _parts;
        
        private void Awake()
        {
            _parts = GetComponentsInChildren<SpriteResolver>().ToDictionary(
                part => part.GetCategory(),
                part => new ModelPart(part));
        }

        public void EquipItem(IEnumerable<ItemPart> itemParts)
        {
            foreach (var part in itemParts)
            {
                _parts[part.Category].SetModel(part.Label);
            }
        }

        public void SetColor(IEnumerable<ItemPart> itemParts, Color color)
        {
            foreach (var part in itemParts)
            {
                _parts[part.Category].SetColor(color);
            }
        }
    }

    public class ModelPart
    {
        private string _category;
        private SpriteResolver _resolver;
        private SpriteRenderer _renderer;

        public ModelPart(SpriteResolver resolver)
        {
            _resolver = resolver;
            _category = _resolver.GetCategory();
            _renderer = _resolver.GetComponent<SpriteRenderer>();
        }
        
        public void SetModel(string model)
        {
            _resolver.SetCategoryAndLabel(_category, model);
        }

        public void SetColor(Color color)
        {
            _renderer.color = color;
        }
    }
}