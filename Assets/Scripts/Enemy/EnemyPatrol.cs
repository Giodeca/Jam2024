using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolSpeed = 2.5f;
    public float patrolDistance = 5f;
    private Vector3 destination;

    private void Start() 
    {
        destination = transform.position - transform.right * patrolDistance;
    }

    private void Update() 
    {
        if(GameManager.Instance.SeeCurrentState() != GameState.TRANSITION)
        {
            transform.position += -transform.right * patrolSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, destination) < 0.1f)
            {
                destination = transform.position + transform.right * patrolDistance;
                transform.eulerAngles += Vector3.up * 180f;
                //Debug.Log(transform.eulerAngles);
            }
        }

    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, destination);
    }
}
