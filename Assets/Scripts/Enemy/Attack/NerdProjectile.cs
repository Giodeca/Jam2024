using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerdProjectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeSpan = 5f;
    public Vector3 direction;
    [SerializeField] int damagePoints;

    void Start()
    {
        Destroy(transform.parent.gameObject, lifeSpan);
    }

    private void Update()
    {
        transform.parent.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable tempdmg))
            //{
            //    print("danno al player");
            //    tempdmg.TakeDamge(damagePoints);
            //}
            Destroy(transform.parent.gameObject);
        }
    }
}
