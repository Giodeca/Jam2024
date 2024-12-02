using System.Collections;
using UnityEngine;

public class NerdEnemyAttack : Enemy
{
    [SerializeField] GameObject projectile;
    [SerializeField] int totalProjToSpawn = 4;
    [SerializeField] Transform[] projSpawnPoints;
    private AIAttackRange _range;
    bool isAttacking;

    [SerializeField] GameObject player;

    protected override void Awake()
    {
        base.Awake();
        _range = GetComponent<AIAttackRange>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void LaunchAttack()
    {
        base.LaunchAttack();
        if (!isAttacking)
        {
            print("launch");
            Vector2 dir = player.transform.position - transform.position;
            if (dir.x < 0)
            {
                dir = Vector2.left;
            }
            else
            {
                dir = Vector2.right;
            }
            print("is attacking");
            isAttacking = true;
            StartCoroutine(AttackCoroutine(dir));
            AudioManager.Instance.PlaySFX("PROJECTILE");
        }
    }

    private IEnumerator AttackCoroutine(Vector2 dir)
    {
        var scale = transform.localScale;
        if (dir == Vector2.right)
        {
            scale.x *= -1;
        }

        int currentProjectiles = totalProjToSpawn;
        int spawnIndex = 0;
        while (currentProjectiles > 0)
        {
            print("proj " + spawnIndex);
            currentProjectiles--;
            GameObject proj = Instantiate(projectile, projSpawnPoints[spawnIndex].position, Quaternion.identity);
            if (dir == Vector2.right)
            {

                proj.transform.localScale = new Vector3(-1, proj.transform.localScale.y, proj.transform.localScale.z);
            }
            proj.GetComponentInChildren<NerdProjectile>().direction = dir;
            spawnIndex++;

            yield return dataEnemy.fireRate;
        }

        yield return dataEnemy.cooldown;
        isAttacking = false;
    }
}
