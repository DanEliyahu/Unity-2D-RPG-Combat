using System;
using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material _whiteFlashMat;
    [SerializeField] private float _restoreDefaultMatTime = 0.2f;

    private Material _defaultMat;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMat = _spriteRenderer.material;
    }
    
    public IEnumerator FlashRoutine(Action callback = null)
    {
        _spriteRenderer.material = _whiteFlashMat;
        yield return new WaitForSeconds(_restoreDefaultMatTime);
        // ReSharper disable once Unity.InefficientPropertyAccess
        _spriteRenderer.material = _defaultMat;

        callback?.Invoke();
    }
}
