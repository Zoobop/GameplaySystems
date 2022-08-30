using System.Collections;
using UnityEngine;

namespace UI
{
    public abstract class Controller : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField, Min(0.0001f)] private float _fadeTime = .5f;
        [SerializeField, Min(0.0001f)] private float _fadeDelay = .0001f;

        private Coroutine _coroutine;
        private WaitForSeconds _tickDelay;

        #region UnityEvents

        protected virtual void Awake()
        {
            _tickDelay = new WaitForSeconds(_fadeDelay);
        }

        #endregion

        public Coroutine Show()
        {
            // Stop if valid
            if (_coroutine != null)
            {
                StopCoroutine(AnimateShow());
            }

            _coroutine = StartCoroutine(AnimateShow());
            return _coroutine;
        }

        public Coroutine Hide()
        {
            // Stop if valid
            if (_coroutine != null)
            {
                StopCoroutine(AnimateHide());
            }

            _coroutine = StartCoroutine(AnimateHide());
            return _coroutine;
        }

        private IEnumerator AnimateShow()
        {
            OnBeginShow();

            yield return AwaitOnBeginShow();
            
            var elapsedTime = 0f;
            while (elapsedTime <= _fadeTime)
            {
                elapsedTime += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(0, 1f, elapsedTime / _fadeTime);
                yield return _tickDelay;
            }

            yield return AwaitOnEndShow();
            
            OnEndShow();
        }

        private IEnumerator AnimateHide()
        {
            OnBeginHide();

            yield return AwaitOnBeginHide();
            
            var elapsedTime = 0f;
            while (elapsedTime <= _fadeTime)
            {
                elapsedTime += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(1f, 0, elapsedTime / _fadeTime);
                yield return _tickDelay;
            }

            yield return AwaitOnEndHide();
            
            OnEndHide();
        }
        
        protected virtual void OnBeginShow() { }
        protected virtual void OnEndShow() { }
        protected virtual void OnBeginHide() { }
        protected virtual void OnEndHide() { }

        protected virtual IEnumerator AwaitOnBeginShow() => null;
        protected virtual IEnumerator AwaitOnEndShow() => null;
        protected virtual IEnumerator AwaitOnBeginHide() => null;
        protected virtual IEnumerator AwaitOnEndHide() => null;
    }
}