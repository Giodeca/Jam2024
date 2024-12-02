using UnityEngine;

public class AnimatorManager : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    private void OnEnable()
    {
        EventManager.AttackAnimator += AttackAnimationEvent;
        EventManager.MovePlayerAnimator += MovePlayerEvent;
        EventManager.ParryAnimator += ParryAnimationEvent;
    }

    private void OnDisable()
    {
        EventManager.AttackAnimator -= AttackAnimationEvent;
        EventManager.MovePlayerAnimator -= MovePlayerEvent;
        EventManager.ParryAnimator -= ParryAnimationEvent;
    }

    private void AttackAnimationEvent()
    {
        animator.SetTrigger("Attack");
    }

    private void ParryAnimationEvent()
    {
        animator.SetTrigger("Parry");
    }

    private void MovePlayerEvent(float value)
    {
        animator.SetFloat("Velocity", value);
    }
}
