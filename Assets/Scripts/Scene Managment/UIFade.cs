using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    [SerializeField] private Image _fadeScreen;
    [SerializeField] private float _fadeSpeed = 1f;

    private Coroutine _fadeRoutine;

    public IEnumerator FadeToBlack()
    {
        if (_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
        }

        _fadeRoutine = StartCoroutine(FadeRoutine(1f));
        yield return _fadeRoutine;
    }

    public IEnumerator FadeToClear()
    {
        if (_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
        }

        _fadeRoutine = StartCoroutine(FadeRoutine(0f));
        yield return _fadeRoutine;
    }


    private IEnumerator FadeRoutine(float targetAlpha)
    {
        while (!Mathf.Approximately(_fadeScreen.color.a, targetAlpha))
        {
            var newAlpha = Mathf.MoveTowards(_fadeScreen.color.a, targetAlpha, _fadeSpeed * Time.deltaTime);
            var color = _fadeScreen.color;
            _fadeScreen.color = new Color(color.r, color.g, color.b, newAlpha);
            yield return null;
        }
    }
}