using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public ScriptableDataEnemy dataEnemy;

    protected int currentHealth;

    public int CurrentHealth { get => currentHealth; }
    public int MaxHealth { get => dataEnemy.health; }

    [SerializeField]
    private float forceAmount = 10f; // Adjust the force amount as needed
    [SerializeField]
    private ForceMode2D forceMode = ForceMode2D.Force; // Change the force mode if necessary
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float secondForStopMoving;
    [SerializeField]
    private float invulnerabilityDuration = 0.25f;
    private Collider2D customCollider;
    public Animator enemyAntimator;
    private AIAttackRange attackRange;
    private EnemyPatrol patrol;

    private void OnEnable()
    {
        EventManager.ReceiveDamage += DamageAnimation;
    }

    private void OnDisable()
    {
        EventManager.ReceiveDamage -= DamageAnimation;
    }

    private void DamageAnimation()
    {

    }

    protected virtual void Awake()
    {
        customCollider = GetComponent<Collider2D>();
        currentHealth = dataEnemy.health;
        enemyAntimator = GetComponent<Animator>();
        attackRange = GetComponent<AIAttackRange>();
        patrol = GetComponent<EnemyPatrol>();
    }

    public virtual void TakeDamge(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(Invulnerability());
        //if(direction > 0)
        //{
        //    forceAmount = -1 * forceAmount;
        //}
        //print(forceAmount);
        //Vector2 forceVector = new Vector2(forceAmount, 0f);

        //rb.AddForce(forceVector, forceMode);
        //StartCoroutine(StopForce());

        if (currentHealth <= 0)
        {
            AudioManager.Instance.PlaySFX(dataEnemy.SoundDeath);
            enemyAntimator.SetTrigger("Die");
            customCollider.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            patrol.enabled = false;
            StopAllCoroutines();
            attackRange.enabled = false;


            Destroy(gameObject, 2f);
            Invoke("LessEnemie", 1f);
        }
        else
        {
            AudioManager.Instance.PlaySFX(dataEnemy.SoundHit);
            enemyAntimator.SetTrigger("Hit");
        }
    }
    IEnumerator Invulnerability()
    {
        gameObject.layer = LayerMask.NameToLayer("Invuln");
        yield return new WaitForSeconds(invulnerabilityDuration);
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    public virtual void LaunchAttack()
    {
        if (dataEnemy.SoundAttack != null && dataEnemy.SoundAttack != "")
        {
            AudioManager.Instance.PlaySFX(dataEnemy.SoundAttack);
        }

        enemyAntimator.SetTrigger("Attack");
    }

    private void LessEnemie()
    {
        EventManager.LessEnemie?.Invoke();
    }

    //IEnumerator StopForce()
    //{
    //    yield return new WaitForSeconds(secondForStopMoving);
    //    if (forceAmount > 0f)
    //    {
    //        forceAmount = -1 * forceAmount;
    //    }
    //    else
    //    {
    //        forceAmount = Mathf.Abs(forceAmount);
    //    }
    //    print("force amount in coroutine" + forceAmount);
    //    rb.AddForce(new Vector2(forceAmount, 0f), forceMode);
    //    rb.velocity = Vector2.zero;
    //    print("coroutine finish");
    //}
}
