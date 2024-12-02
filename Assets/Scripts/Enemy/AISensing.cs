using UnityEngine;

public class AISensing : MonoBehaviour
{
    //Quando entra nel box collider vede player

    [field: SerializeField]
    public bool PlayerDetected { get; private set; }
    [SerializeField] FollowPlayer follow;

    public Vector2 detectionSize = Vector2.one;
    public Vector2 detectionOriginOffset = Vector2.zero;

    public float detectionDelay = 0.3f;
    public LayerMask detectionLayerMask;
    private Collider2D target;

    [Header("Gizmo Parameters")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectionColor = Color.green;
    public bool showGizmo = true;

    EnemyPatrol _patrol;

    private void Awake()
    {
        follow = GetComponent<FollowPlayer>();
        _patrol = GetComponent<EnemyPatrol>();
        follow.enabled = false;
    }

    private void Update()
    {
        if (target = Physics2D.OverlapBox(transform.position, detectionSize, 0, detectionLayerMask))
        {
            follow.player = target.gameObject;
            follow.canFollow = true;
            follow.enabled = true;
            _patrol.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmo && transform.position != null)
        {
            Gizmos.color = gizmoIdleColor;
            if (PlayerDetected)
            {
                Gizmos.color = gizmoDetectionColor;
            }
            Gizmos.DrawWireCube((Vector2)transform.position + detectionOriginOffset, detectionSize);
        }
    }

    public void StopSensing()
    {
        follow.canFollow = false;
        this.enabled = false;
        //follow.enabled = false;
    }
}
