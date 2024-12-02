using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(transform.parent.tag) && collision.TryGetComponent(out IParriable parriable))
        {
            parriable.GotParried();
        }

    }
}
