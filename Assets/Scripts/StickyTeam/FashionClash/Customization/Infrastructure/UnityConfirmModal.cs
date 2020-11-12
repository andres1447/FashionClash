using System;
using UniRx;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

namespace StickyTeam.FashionClash.Customization.Infrastructure
{
    public abstract class UnityConfirmModal<T> : MonoBehaviour
    {
        private readonly int SHOW = Animator.StringToHash("Show");
        private readonly int HIDE = Animator.StringToHash("Hide");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private Button _confirm;
        [SerializeField] private Button _cancel;

        private IObservable<bool> _response;
        
        protected void Awake()
        {
            _response = _confirm.onClick.AsObservable().Select(_ => true).Concat(
                _cancel.onClick.AsObservable().Select(_ => false));
        }

        protected abstract void Map(T data);
        
        public IObservable<bool> Show(T data)
        {
            Map(data);
            _animator.SetTrigger(SHOW);
            return _response.Take(1).Finally(Hide);
        }

        private void Hide()
        {
            _animator.SetTrigger(HIDE);
        }
    }
}