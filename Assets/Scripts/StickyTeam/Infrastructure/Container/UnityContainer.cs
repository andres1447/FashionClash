using System.Collections.Generic;
using UnityEngine;

namespace StickyTeam.Infrastructure.Container
{
    public class UnityContainer : MonoBehaviour
    {
        [SerializeField] private List<UnityModule> _modules;

        [SerializeField] private Transform _canvas;
        [SerializeField] private List<GameObject> _views;

        public static Resolver Resolver { get; private set; }

        private void Awake()
        {
            Resolver =  new Resolver();
            foreach (var module in _modules)
            {
                module.Register(Resolver);
            }

            foreach (var view in _views)
            {
                Instantiate(view, _canvas);
            }
        }
    }
}