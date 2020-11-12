using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace StickyTeam.FashionClash.Customization.Infrastructure
{
    [RequireComponent(typeof(Button))]
    public abstract class UnitySelectableWidget<T> : MonoBehaviour
    {
        private T _data;

        public IObservable<T> OnSelected { get; protected set; }

        protected void Awake()
        {
            OnSelected = GetComponent<Button>().onClick.AsObservable().Select(ReturnData);
        }

        private T ReturnData(Unit unit)
        {
            return _data;
        }

        protected abstract void Map(T data);

        public void Display(T data)
        {
            _data = data;
            gameObject.SetActive(true);
            Map(data);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}