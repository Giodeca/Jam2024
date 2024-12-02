using System.Collections;
using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] GameObject projectile;
    [SerializeField] int totalProjToSpawn = 5;
    [SerializeField] Transform[] projSpawnPoints;
    bool isAttacking;

    [SerializeField] GameObject player;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void TakeDamge(int damage)
    {
        base.TakeDamge(damage);
        if (currentHealth <= 0)
        {
            GameManager.Instance.ChangeState(GameState.WIN);
        }
    }

    public override void LaunchAttack()
    {
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 0)
        {
            BellyAttack();
        }
        else
        {
            ProjectileAttack();
        }
    }

    private void BellyAttack()
    {
        print("belly");
        enemyAntimator.SetTrigger("Attack");
    }

    private void ProjectileAttack()
    {
        print("proj");
        Vector2 dir = player.transform.position - transform.position;
        if (dir.x < 0)
        {
            dir = Vector2.left;
        }
        else
        {
            dir = Vector2.right;
        }
        isAttacking = true;
        StartCoroutine(ProjectileCoroutine(dir));
    }

    private IEnumerator ProjectileCoroutine(Vector2 dir)
    {
        enemyAntimator.SetTrigger("Projectile");
        int currentProjectiles = totalProjToSpawn;
        int spawnIndex = 0;
        while (currentProjectiles > 0)
        {
            currentProjectiles--;
            GameObject proj = Instantiate(projectile, projSpawnPoints[spawnIndex].position, Quaternion.identity);
            if (dir == Vector2.right)
            {
                proj.transform.localScale = new Vector3(-1, proj.transform.localScale.y, proj.transform.localScale.z);
            }
            proj.GetComponentInChildren<NerdProjectile>().direction = dir;
            spawnIndex++;

            yield return new WaitForSeconds(dataEnemy.fireRate);
        }
    }
}
