using System;
using System.Collections;
using UnityEngine;

namespace UI
{
    public abstract class ContentWindow<T> : MonoBehaviour, IContentWindow<T>
    {
        [Header("References")] [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField, Min(0.0001f)] private float _fadeTime = .1f;
        [SerializeField, Min(0.0001f)] private float _fadeDelay = .0001f;

        private Coroutine _windowCoroutine;
        private bool _isOpen;
        private Canvas _canvas;
        private WaitForSeconds _tickDelay;

        public event Action OnWindowClosed = delegate { };

        #region UnityEvents

        protected virtual void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _tickDelay = new WaitForSeconds(_fadeDelay);
        }

        #endregion

        #region IWindow

        public virtual void Select(IWindowElement element)
        {
        }

        public virtual void Deselect()
        {
        }

        public bool IsWindowOpen()
        {
            return _isOpen;
        }

        #endregion

        #region IContentWindow

        public void OpenWindow(T content)
        {
            if (_windowCoroutine != null)
            {
                StopCoroutine(Show(content));
            }

            _windowCoroutine = StartCoroutine(Show(content));
        }

        public void CloseWindow()
        {
            if (_windowCoroutine != null)
            {
                StopCoroutine(Hide());
            }

            _windowCoroutine = StartCoroutine(Hide());
            OnWindowClosed?.Invoke();
        }

        #endregion

        #region AnimationUtility

        protected virtual IEnumerator Show(T content)
        {
            yield return AnimateShow();
        }

        protected virtual IEnumerator Hide()
        {
            yield return AnimateHide();
        }

        private IEnumerator AnimateShow()
        {
            var elapsedTime = 0f;
            while (elapsedTime <= _fadeTime)
            {
                elapsedTime += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(0, 1f, elapsedTime / _fadeTime);
                yield return _tickDelay;
            }
        }

        private IEnumerator AnimateHide()
        {
            var elapsedTime = 0f;
            while (elapsedTime <= _fadeTime)
            {
                elapsedTime += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(1f, 0, elapsedTime / _fadeTime);
                yield return _tickDelay;
            }
        }

        #endregion

        protected void SetWindowOpen(bool state)
        {
            _isOpen = state;
        }

        protected void SetCanvas(bool state)
        {
            _canvas.enabled = state;
        }
    }
}