using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace StickyTeam.Infrastructure
{
    public abstract class UnityConfirmModal<T> : MonoBehaviour
    {
        private readonly int SHOW = Animator.StringToHash("Show");
        private readonly int HIDE = Animator.StringToHash("Hide");
        
        [SerializeField] private Animator _animator;
        
        [SerializeField] private Button _confirm;
        public Button Confirm => _confirm;
        
        [SerializeField] private Button _cancel;
        public Button Cancel => _cancel;

        private IObservable<bool> _response;
        
        private void Awake()
        {
            _response = _confirm.OnClickAsObservable().Select(_ => true).Merge(_cancel.OnClickAsObservable().Select(_ => false));
            gameObject.SetActive(false);
        }

        protected abstract void Map(T data);
        
        public IObservable<bool> Show(T data)
        {
            Map(data);
            gameObject.SetActive(true);
            //_animator.SetTrigger(SHOW);
            return _response.Take(1).Finally(Hide);
        }

        public void Hide()
        {
            //_animator.SetTrigger(HIDE);
            gameObject.SetActive(false);
        }
    }
}