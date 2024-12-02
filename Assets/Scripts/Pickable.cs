using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickable : MonoBehaviour, IPickable
{
    [SerializeField]
    private PickableData dataPickable;

    private GameObject tempGMB;

    public bool canKill = false;

    
    private void Update()
    {
        if(tempGMB != null)
        {
            transform.position = tempGMB.transform.position;
        }
    }
    public void BeTake(GameObject positionToGo)
    {
        tempGMB = positionToGo; 
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        EventManager.ReferenceObjectPick?.Invoke(this.gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canKill)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable tempdmg))
                {
                    tempdmg.TakeDamge(dataPickable.damagePick);
                    Destroy(this.gameObject);
                }

            }
        }

    }

}
