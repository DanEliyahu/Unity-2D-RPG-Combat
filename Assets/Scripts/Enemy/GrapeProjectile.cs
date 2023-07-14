using System.Collections;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private float _duration = 1f;
    [SerializeField] private AnimationCurve _animCurve;
    [SerializeField] private float _maxHeight = 3f;
    [SerializeField] private Transform _grapeSprite;

    private Vector2 _grapeSpriteStartLocalPos;
    private void Start()
    {
        _grapeSpriteStartLocalPos = _grapeSprite.localPosition;
        StartCoroutine(ProjectileCurveRoutine(transform.position, PlayerController.Instance.transform.position));
    }
    
    private IEnumerator ProjectileCurveRoutine(Vector2 startPos, Vector2 endPos)
    {
        float timePassed = 0f;
        Vector2 heightAddition = Vector2.zero;

        while (timePassed < _duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / _duration;
            float heightT = _animCurve.Evaluate(linearT);
            float currentHeight = Mathf.Lerp(0f, _maxHeight, heightT);
            heightAddition.y = currentHeight;

            transform.position = Vector2.Lerp(startPos, endPos, linearT);
            _grapeSprite.localPosition = _grapeSpriteStartLocalPos + heightAddition;
            
            yield return null;
        }
        
        Destroy(gameObject);
    }

}
