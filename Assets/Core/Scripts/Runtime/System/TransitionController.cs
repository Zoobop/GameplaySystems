using System.Collections;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public static TransitionController Instance { get; private set; }

    [Header("References")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField, Min(0.0001f)] private float _fadeTime = .75f;
    [SerializeField, Min(0.0001f)] private float _fadeDelay = .0001f;

    private Coroutine _coroutine;
    private WaitForSeconds _tickDelay;
    
    #region UnityEvents

    protected virtual void Awake()
    {
        Instance = this;
        _tickDelay = new WaitForSeconds(_fadeDelay);
    }

    #endregion
    
    public static IEnumerator TransitionAsync(float start, float stop, float fadeTime)
    {
        var canvasGroup = Instance._canvasGroup;
        var tickDelay = Instance._tickDelay;

        var elapsedTime = 0f;
        while (elapsedTime <= fadeTime)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, stop, elapsedTime / fadeTime);
            yield return tickDelay;
        }
    }

    public static IEnumerator StartTransition()
    {
        Enable();
        
        yield return TransitionAsync(0f, 1f, Instance._fadeTime);
    }

    public static IEnumerator StopTransition()
    {
        yield return TransitionAsync(1f, 0f, Instance._fadeTime);
        
        Disable();
    }
    
    public static IEnumerator StartTransition(float time)
    {
        Enable();
        
        yield return TransitionAsync(0f, 1f, time);
    }

    public static IEnumerator StopTransition(float time)
    {
        yield return TransitionAsync(1f, 0f, time);
        
        Disable();
    }

    private static void Enable()
    {
        Instance._canvas.enabled = true;
    }

    private static void Disable()
    {
        Instance._canvas.enabled = false;
    }
}