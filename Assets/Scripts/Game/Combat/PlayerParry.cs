using System.Collections;
using UnityEditor;
using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField] float parryCooldown = 1f;
    bool canParry = true;

    private void OnEnable()
    {
        EventManager.ParryAction += ReadParry;
    }
    private void OnDisable()
    {
        EventManager.ParryAction -= ReadParry;
    }
    private void ReadParry()
    {
        if (canParry) StartCoroutine(ParryCooldown());
    }

    private IEnumerator ParryCooldown()
    {
        canParry = false;
        Debug.Log("Parry");
        animator.SetTrigger("Parry");
        yield return new WaitForSeconds(parryCooldown);
        canParry = true;
    }
}
