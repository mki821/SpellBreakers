using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour, ICharacterComponent
{
    private readonly int IsMovingHash = Animator.StringToHash("IsMoving");

    protected Animator _animator;

    public void Initialize(Character owner)
    {
        _animator = GetComponent<Animator>();
    }

    public void SetMoving(bool isMoving)
    {
        _animator.SetBool(IsMovingHash, isMoving);
    }
}
