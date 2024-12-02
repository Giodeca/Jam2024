using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    //[SerializeField] DoDamage hitbox1;
    //[SerializeField] DoDamage hitbox2;

    private void OnEnable()
    {
        EventManager.AttackPerformed += ReadAttack;
        EventManager.FirstAttackPerformed += OnFirstAttack;
        EventManager.ComboAttackPerformed += OnFullCombo;
    }
    private void OnDisable()
    {
        EventManager.AttackPerformed -= ReadAttack;
        EventManager.FirstAttackPerformed -= OnFirstAttack;
        EventManager.ComboAttackPerformed -= OnFullCombo;
    }
    private void ReadAttack()
    {
        animator.SetTrigger("FirstAttack");
        animator.SetBool("FullCombo", false);

    }

    private void OnFirstAttack()
    {
        // first combo hit
        //animator.SetTrigger("FirstAttck");
    }

    private void OnFullCombo()
    {
        animator.SetBool("FullCombo", true);
    }

    public void PlaySoundHitbox1()
    {

        AudioManager.Instance.PlaySFX("PUNCH1");
    }

    public void PlaySoundHitbox2()
    {

        AudioManager.Instance.PlaySFX("PUNCH2");
    }
}
