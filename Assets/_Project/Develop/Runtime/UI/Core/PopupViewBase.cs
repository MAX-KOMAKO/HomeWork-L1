using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Develop.Runtime.UI.Core
{
    public abstract class PopupViewBase : MonoBehaviour, IShowableView
    {
        public event Action CloseRequest;

        [SerializeField] private CanvasGroup _mainGroup;
        [SerializeField] private CanvasGroup _body;
        
        [SerializeField] private float _animationDuration = 0.2f;

        private void Awake()
        {
            _mainGroup.alpha = 0;
        }

        public void OnCloseButtonClicked() => CloseRequest?.Invoke();

        public IEnumerator Show()
        {
            OnPreShow();
            _mainGroup.alpha = 1;

            float elapsed = 0f;
            while (elapsed < _animationDuration)
            {
                elapsed += Time.deltaTime;
                _body.alpha = Mathf.Lerp(0f, 1f, elapsed / _animationDuration);
                yield return null;
            }
            _body.alpha = 1f;
            
            OnPostShow();
        }

        public IEnumerator Hide()
        {
            OnPreHide();

            float elapsed = 0f;
            while (elapsed < _animationDuration)
            {
                elapsed += Time.deltaTime;
                _body.alpha = Mathf.Lerp(1f, 0f, elapsed / _animationDuration);
                yield return null;
            }
            _body.alpha = 0f;
            _mainGroup.alpha = 0f;
            
            OnPostHide();
        }

        protected virtual void OnPostShow() { }
        protected virtual void OnPreShow() { }
        protected virtual void OnPostHide() { }
        protected virtual void OnPreHide() { }
    }
}