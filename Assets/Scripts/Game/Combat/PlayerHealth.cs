using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public int health;
    [SerializeField] int currentHealth;
    [SerializeField] private Animator animator;
    PlayerMovement playerMovement;
    PlayerAttack attack;

    public int CurrentHealth { get => currentHealth; }
    public int MaxHealth { get => health; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
    }
    private void Start()
    {
        currentHealth = health;
    }

    public void TakeDamge(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit");
        StartCoroutine(Invulnerability());

        if (currentHealth <= 0 && GameManager.Instance.SeeCurrentState() != GameState.GAMEOVER)
        {
            AudioManager.Instance.PlaySFX("DEATH");
            currentHealth = 0;
            GameManager.Instance.ChangeState(GameState.GAMEOVER);
            Death();
        }
    }

    IEnumerator Invulnerability()
    {
        gameObject.layer = LayerMask.NameToLayer("Invuln");
        yield return new WaitForSeconds(1);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void Death()
    {

        animator.SetBool("Death", true);
        playerMovement.enabled = false;
        attack.enabled = false;

    }

    public void Immortal()
    {
        currentHealth = 20000;
        health = 20000;
    }

    public void Normal()
    {
        currentHealth = 300;
        health = 300;
    }

}
