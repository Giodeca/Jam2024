using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    
    public float speed = 3;
    public GameObject player { get; set; }
    public bool canFollow { get; set; }
    public Vector3 direction { get; private set; }

    Enemy enemyScript;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyScript = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        
        direction = player.transform.position - transform.position;
        direction = new Vector3(direction.normalized.x, 0f, 0f);

        if (canFollow)
        {
            enemyScript.enemyAntimator.SetBool("Move", true);
            enemyScript.enemyAntimator.SetBool("Wait", false);
            transform.position += direction * speed * Time.deltaTime;
        }
            
            

        transform.rotation = Quaternion.LookRotation(direction);
        Vector3 angles = transform.eulerAngles;
        angles.y += 90f;
        transform.eulerAngles = angles;
    }
}
