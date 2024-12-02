using JetBrains.Annotations;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float dashSpeed;
    bool isDashing;
    [SerializeField] float _gravity;
    [SerializeField] float jumpHight;
    [SerializeField] float jumpSpeed;
    Rigidbody2D _rb;
    bool isFacingRight;
    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask WallLayer;

    bool isGrounded;
    Vector2 moveVelocity;

    InputMovement _input;
    InputAction _moveAction;

    public Animator _animator;

    private bool canPick = false;

    [SerializeField]
    private GameObject positionForPicks;

    private GameObject tempPickObject;

    [SerializeField]
    private float impulseThrwoeObjectX;
    [SerializeField]
    private float impulseThrwoeObjectY;
    [SerializeField]
    private float gravityObject;
    private bool isIdleReproduce = false;
    private bool isRunReproduce = false;
    private bool idleCoroutineStarted;
    private bool runCoroutineStarted;

    private Coroutine idleCoroutine;
    private Coroutine runCoroutine;

    private GameManager gm_instance;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = new InputMovement();
        _animator = GetComponent<Animator>();

    }
    private void Start()
    {
        gm_instance = GameManager.Instance;
    }

    private void OnEnable()
    {
        _input.Enable();
        _moveAction = _input.Movement.Move;
        _input.Movement.Jump.performed += ctx => Jump();
        //_input.Movement.Crouch.performed += ctx => Crouch();
        //_input.Movement.Crouch.canceled += ctx => Uncrouch();
        _input.Movement.Dash.performed += ctx => { isDashing = true; };
        _input.Movement.Dash.canceled += ctx => { isDashing = false; };
        


        EventManager.PickAction += CollectItem;
        EventManager.ReferenceObjectPick += PassReference;
        EventManager.ThrowAction += ThrowEvent;

    }
    private void CollectItem()
    {
        print("can pick");
        canPick = true;
        StartCoroutine(DismissPick());
    }

    private void OnDisable()
    {
        _input.Disable();
        EventManager.PickAction -= CollectItem;
        EventManager.ReferenceObjectPick -= PassReference;
        EventManager.ThrowAction -= ThrowEvent;
    }

    private void ThrowEvent()
    {
        if(tempPickObject != null)
        {
            AudioManager.Instance.PlaySFX("THROW");
            tempPickObject.GetComponent<Pickable>().enabled = false;
            Rigidbody2D tempRb = tempPickObject.GetComponent<Rigidbody2D>();
            Collider2D tempColl = tempPickObject.GetComponent<Collider2D>();
            Pickable pickScript = tempPickObject.GetComponent<Pickable>();
            pickScript.canKill = true;
            tempColl.enabled = true;
            tempRb.AddForce(new Vector3(impulseThrwoeObjectX, impulseThrwoeObjectY), ForceMode2D.Impulse);
            tempRb.gravityScale = gravityObject;
            tempPickObject = null;
        }
    }

    private void PassReference(GameObject tempobj)
    {
        tempPickObject = tempobj;
    }

    void Update()
    {
        if(gm_instance.SeeCurrentState() != GameState.TRANSITION && gm_instance.SeeCurrentState() != GameState.PAUSE)
        {
            _animator.SetFloat("Yvelocity", _rb.velocity.y);
            _animator.SetBool("isGrounded", isGrounded);

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);

            moveVelocity = _moveAction.ReadValue<Vector2>();

            // Cast a ray to the left
            RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, raycastDistance, WallLayer);
            Debug.DrawRay(transform.position, Vector2.left * raycastDistance, leftHit.collider != null ? Color.red : Color.green);

            // Cast a ray to the right
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, raycastDistance, WallLayer);
            Debug.DrawRay(transform.position, Vector2.right * raycastDistance, rightHit.collider != null ? Color.red : Color.green);

            // Check if either ray hits an obstacle
            if (leftHit.collider != null && moveVelocity.x == -1)
            {
                moveVelocity.x = 0;
            }
            else if (rightHit.collider != null && moveVelocity.x == 1)
            {
                moveVelocity.x = 0;
            }

            _rb.velocity = new Vector2(moveVelocity.x * moveSpeed, _rb.velocity.y);

            Animations();

            if (isFacingRight && _rb.velocity.x < 0)
            {
                flip();
            }
            else if (!isFacingRight && _rb.velocity.x > 0)
            {
                flip();
            }

            EventManager.MovePlayerAnimator?.Invoke(Mathf.Abs(_rb.velocity.x));
        }
       

    }

    private void Animations()
    {
        if (_rb.velocity.x != 0)
        {
            _animator.SetBool("running", true);
            //isRunReproduce = false;
            //isIdleReproduce = true;

            //runCoroutine = StartCoroutine(CheckIfRun());
            //if(idleCoroutine != null)
            //{
            //    StopCoroutine(idleCoroutine);
            //}

            idleCoroutineStarted = false;
        }
        else
        {

            _animator.SetBool("running", false);
            //isIdleReproduce= false;
            //isRunReproduce = true;
            //idleCoroutine = StartCoroutine(ChechkStillIdle());
            //if (runCoroutine != null)
            //{
            //    StopCoroutine(runCoroutine);
            //}

            //runCoroutineStarted = false;

        }
    }

    IEnumerator ChechkStillIdle()
    {
        if(!idleCoroutineStarted)
        {
            idleCoroutineStarted = true;
            yield return new WaitForSeconds(2f);
            if (!isIdleReproduce)
            {
                AudioManager.Instance.PlaySFX("IDLE", 1);
            }
        }

    }
    IEnumerator CheckIfRun()
    {
        if (!runCoroutineStarted)
        {
            runCoroutineStarted = true;
            yield return new WaitForSeconds(1f);
            if (!isRunReproduce)
            {
                AudioManager.Instance.PlaySFX("RUN", 1);
            }
        }

    }
    private void Jump()
    {
        if (isGrounded && gm_instance.SeeCurrentState() != GameState.TRANSITION && gm_instance.SeeCurrentState() != GameState.PAUSE)
        {
            AudioManager.Instance.PlaySFX("JUMP");
            //Uncrouch();
            _rb.gravityScale = _gravity;
            float jumpForce = Mathf.Sqrt(jumpHight * (Physics2D.gravity.y * _rb.gravityScale) * -2) * _rb.mass;

            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);

        }
    }

    //private void Crouch()
    //{
    //    if (isGrounded)
    //    {
    //        transform.position -= new Vector3(0f, GetComponent<CapsuleCollider2D>().size.y / 2, 0f);
    //        transform.localScale = new Vector3(1f, 0.5f, 1f);
    //    }
    //}

    //private void Uncrouch()
    //{
    //    transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);
    //}
    private void flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IPickable>(out IPickable tempPick))
        {
            if (canPick)
            {
                AudioManager.Instance.PlaySFX("COLLECTED");
                if(tempPickObject != null)
                {
                    Destroy(tempPickObject);
                }
                tempPick.BeTake(positionForPicks);
                canPick = false;
            }
        }
    }

    IEnumerator DismissPick()
    {
        yield return new WaitForSeconds(0.1f);
        canPick = false;
    }
}
