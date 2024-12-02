using UnityEngine;

public class AIAttackRange : MonoBehaviour
{
    //Quando entra nel box collider vede player

    [field: SerializeField]
    public bool PlayerInRange { get; private set; }

    public Vector2 detectionSize = Vector2.one;
    public Vector2 detectionOriginOffset = Vector2.zero;

    public float detectionDelay = 0.3f;
    public LayerMask detectionLayerMask;

    [Header("Gizmo Parameters")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectionColor = Color.green;
    public bool showGizmo = true;

    AISensing _sensing;

    Rigidbody2D rb;

    Enemy enemyScript;
    private bool canAttacck;
    private float insideTimer;

    private void Awake()
    {
        _sensing = GetComponent<AISensing>();
        enemyScript = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        insideTimer = enemyScript.dataEnemy.cooldown;
    }

    private void Update()
    {
        if (Physics2D.OverlapBox(transform.position, detectionSize, 0, detectionLayerMask))
        {
            _sensing.StopSensing();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            enemyScript.enemyAntimator.SetBool("Move", false);
            enemyScript.enemyAntimator.SetBool("Wait", true);
            insideTimer += Time.deltaTime;
            if (insideTimer >= enemyScript.dataEnemy.cooldown)
            {
                enemyScript.LaunchAttack();
                insideTimer = 0;
            }
        }
        else
        {
            _sensing.enabled = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmo && transform != null)
        {
            Gizmos.color = gizmoIdleColor;
            if (PlayerInRange)
            {
                Gizmos.color = gizmoDetectionColor;
            }
            Gizmos.DrawWireCube((Vector2)transform.position + detectionOriginOffset, detectionSize);
        }
    }


    private void OnDisable()
    {
        _sensing.StopSensing();
    }
}
