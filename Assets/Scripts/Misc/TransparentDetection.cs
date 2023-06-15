using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float _targetTransparency = 0.7f;
    [SerializeField] private float _fadeTime = 0.2f;

    private SpriteRenderer _spriteRenderer;
    private Tilemap _tilemap;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            if (_spriteRenderer)
            {
                StartCoroutine(FadeRoutine(_spriteRenderer, _spriteRenderer.color.a, _targetTransparency));
            }
            else if (_tilemap)
            {
                StartCoroutine(FadeRoutine(_tilemap, _tilemap.color.a, _targetTransparency));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            if (_spriteRenderer)
            {
                StartCoroutine(FadeRoutine(_spriteRenderer, _spriteRenderer.color.a, 1f));
            }
            else if (_tilemap)
            {
                StartCoroutine(FadeRoutine(_tilemap, _tilemap.color.a, 1f));
            }
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float startValue, float targetValue)
    {
        float elapsedTime = 0;
        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetValue, elapsedTime / _fadeTime);
            var color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, newAlpha);
            yield return null;
        }
    }
    
    private IEnumerator FadeRoutine(Tilemap tilemap, float startValue, float targetValue)
    {
        float elapsedTime = 0;
        while (elapsedTime < _fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetValue, elapsedTime / _fadeTime);
            var color = tilemap.color;
            tilemap.color = new Color(color.r, color.g, color.b, newAlpha);
            yield return null;
        }
    }
}
