using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 lastDirection = Vector2.down;

    private float idleTimer;
    private float blinkDelay = 5f;

    public Vector2 lastMotionVector;
    //public bool moving;

    //[SerializeField] protected float maxHp = 100f;
    //protected float currentHp;
    //[Seriali  zeField] protected Image hpBar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        MovePlayer();
        HandleBlink();

        //moving = horizontal != 0 || vertical != 0;
        //animator.SetBool("moving", moving);
        if (horizontal != 0 || vertical != 0)
        {
            lastMotionVector = new Vector2(horizontal, vertical);
            animator.SetFloat("LastHorizontal", horizontal);
            animator.SetFloat("LastVertical", vertical);
        }
    }

    void MovePlayer()
    {
        Vector2 rawInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (rawInput.magnitude < 0.1f)
        {
            rawInput = Vector2.zero;
        }

        Vector2 playerInput = Vector2.zero;

        // Chỉ xử lý hướng khi thực sự có input
        if (rawInput != Vector2.zero)
        {
            if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y))
            {
                playerInput = new Vector2(Mathf.Sign(rawInput.x), 0);
            }
            else
            {
                playerInput = new Vector2(0, Mathf.Sign(rawInput.y));
            }
        }

        bool isMoving = playerInput != Vector2.zero;

        if (isMoving)
        {
            lastDirection = playerInput;

            animator.SetFloat("moveX", playerInput.x);
            animator.SetFloat("moveY", playerInput.y);
        }
        else
        {
            animator.SetFloat("moveX", lastDirection.x);
            animator.SetFloat("moveY", lastDirection.y);
        }

        bool isRunning = isMoving && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isRunning", isRunning);

        float currentSpeed = isRunning ? moveSpeed * 1.7f : moveSpeed;

        if (rawInput != Vector2.zero)
        {
            rb.linearVelocity = rawInput.normalized * currentSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        //if (playerInput.x != 0)
        //{
        //    spriteRenderer.flipX = playerInput.x < 0;
        //}

        if (isMoving)
        {
            idleTimer = 0f;
            animator.SetBool("isBlinking", false);
        }
    }

    void HandleBlink()
    {
        if (!animator.GetBool("isMoving"))
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= blinkDelay)
            {
                animator.SetBool("isBlinking", true);
            }
        }
    }

    //public virtual void TakeDamage(float damage)
    //{
    //    currentHp -= damage;
    //    currentHp = Mathf.Max(currentHp, 0);
    //    UpdateHpBar();
    //    if (currentHp <= 0)
    //    {
    //        Die();
    //    }
    //}

    //protected virtual void Die()
    //{
    //    Destroy(gameObject);
    //}

    //protected void UpdateHpBar()
    //{
    //    if (hpBar != null)
    //    {
    //        hpBar.fillAmount = currentHp / maxHp;
    //    }
    //}
}
