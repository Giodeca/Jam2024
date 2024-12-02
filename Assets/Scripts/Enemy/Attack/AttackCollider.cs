using UnityEngine;

public class AttackCollider : MonoBehaviour, IParriable
{
    public void GotParried()
    {
        Debug.Log("Parried");
    }
}
