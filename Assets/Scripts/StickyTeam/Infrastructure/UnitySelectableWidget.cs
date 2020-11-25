using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace StickyTeam.Infrastructure
{
    [RequireComponent(typeof(Button))]
    public abstract class UnitySelectableWidget<T> : MonoBehaviour
    {
        private T _data;

        public IObservable<T> OnSelected { get; private set; }

        protected void Awake()
        {
            OnSelected = GetComponent<Button>().OnClickAsObservable().Select(ReturnData);
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