using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomIdleAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        _animator.Play(animatorStateInfo.fullPathHash, 0, Random.Range(0f, 1f));
    }
}
