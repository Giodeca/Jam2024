using UnityEngine;

public class AttackSMB : StateMachineBehaviour
{

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var collider in animator.transform.GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }
    }
}
