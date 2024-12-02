using UnityEngine;

public class DoDamage : MonoBehaviour, IDoDamage, IParriable
{
    [SerializeField]
    protected int damagePoints = 100;

    [SerializeField]
    private string hitSuccessfulSound;
    [SerializeField]
    private string hitFailedSound;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(transform.parent.tag))
        {
            DoDamageToSomething(damagePoints, collision);
        }


    }

    public void DoDamageToSomething(int damage, Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable tempdmg))
        {
            tempdmg.TakeDamge(damagePoints);
            if (hitSuccessfulSound != "")
            {
                AudioManager.Instance.PlaySFX(hitSuccessfulSound, 1);
            }
        }
    }

    public void GotParried()
    {
        print(transform.parent.name + "'s attack got parried");
    }

    public void PlayEmptyAttackSound()
    {
        if (hitFailedSound != "")
        {
            AudioManager.Instance.PlaySFX(hitFailedSound, 1);
        }
    }
}
