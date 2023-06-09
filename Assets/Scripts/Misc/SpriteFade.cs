using System.Collections;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 0.4f;
    [SerializeField] private bool _fadeOnStart;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (_fadeOnStart)
        {
            StartCoroutine(FadeRoutine());
        }
    }

    public IEnumerator FadeRoutine()
    {
        float elapsedTime = 0;
        var newSpriteColor = _spriteRenderer.color;
        float startValue = newSpriteColor.a;

        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;

            newSpriteColor.a = Mathf.Lerp(startValue, 0f, elapsedTime / _fadeTime);
            _spriteRenderer.color = newSpriteColor;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
