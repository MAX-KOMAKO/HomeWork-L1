using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _Project.Develop.Runtime.UI.Core
{
    public abstract class PopupViewBase : MonoBehaviour, IShowableView
    {
        public event Action CloseRequest;

        [SerializeField] private CanvasGroup _mainGroup;
        [SerializeField] private CanvasGroup _body;
        
        [SerializeField] private float _animationDuration = 0.2f;

        private Tween _currentTween;

        private void Awake()
        {
            _mainGroup.alpha = 0;
        }

        private void OnDestroy()
        {
            _currentTween?.Kill();
        }

        public void OnCloseButtonClicked() => CloseRequest?.Invoke();

        public IEnumerator Show()
        {
            OnPreShow();
            _mainGroup.alpha = 1;

            _currentTween?.Kill();
            _currentTween = _body.DOFade(1f, _animationDuration).SetUpdate(true);
            
            yield return _currentTween.WaitForCompletion();
            
            OnPostShow();
        }

        public IEnumerator Hide()
        {
            OnPreHide();

            _currentTween?.Kill();
            _currentTween = _body.DOFade(0f, _animationDuration).SetUpdate(true);
            
            yield return _currentTween.WaitForCompletion();
            
            _mainGroup.alpha = 0f;
            
            OnPostHide();
        }

        protected virtual void OnPostShow() { }
        protected virtual void OnPreShow() { }
        protected virtual void OnPostHide() { }
        protected virtual void OnPreHide() { }
    }
}