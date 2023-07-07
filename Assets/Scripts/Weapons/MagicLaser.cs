using System;
using System.Collections;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float _laserRange = 12f;
    [SerializeField] private float _laserGrowTime = 2f;
    
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _capsuleCollider;
    private SpriteFade _spriteFade;

    private bool _isGrowing = true;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _spriteFade = GetComponent<SpriteFade>();
    }

    private void Start()
    {
        LaserFaceMouse();
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Indestructible") && !other.isTrigger)
        {
            _isGrowing = false;
        }
    }

    private void LaserFaceMouse()
    {
        var mousePosition = CameraController.Instance.MainCam.ScreenToWorldPoint(Input.mousePosition);

        var laserTransform = transform;
        Vector2 direction = mousePosition - laserTransform.position;
        laserTransform.right = direction;
    }

    private IEnumerator IncreaseLaserLengthRoutine()
    {
        var spriteRendererStartSize = _spriteRenderer.size;
        var colliderStartSize = _capsuleCollider.size;
        var colliderStartOffset = _capsuleCollider.offset;
        
        Vector2 newSpriteSize = spriteRendererStartSize;
        Vector2 newColliderSize = colliderStartSize;
        Vector2 newColliderOffset = colliderStartOffset;

        float targetColliderXSize = _laserRange - (spriteRendererStartSize.x - colliderStartSize.x);
        float targetColliderXOffset = _laserRange * 0.5f;
        var timePassed = 0f;
        
        while (_spriteRenderer.size.x < _laserRange && _isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / _laserGrowTime;
            
            newSpriteSize.x = Mathf.Lerp(spriteRendererStartSize.x, _laserRange, linearT);
            newColliderSize.x = Mathf.Lerp(colliderStartSize.x, targetColliderXSize, linearT);
            newColliderOffset.x = Mathf.Lerp(colliderStartOffset.x, targetColliderXOffset, linearT);
            
            _spriteRenderer.size = newSpriteSize;
            _capsuleCollider.size = newColliderSize;
            _capsuleCollider.offset = newColliderOffset;
            
            yield return null;
        }

        StartCoroutine(_spriteFade.FadeRoutine());
    }
}
