using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent playerDied = new UnityEvent();

    #region Movement
    [Header("Movement")]
    [SerializeField] private float speed;
    private bool facingRight = true;
    #endregion

    #region DashMovement
    [Header("DashMovement")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float startDashTime;
    private float dashTime;
    #endregion

    #region Jump
    [Header("Jump")]
    [SerializeField] private float jumpPower;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.02f;
    [SerializeField] private int extraJumpsValue;
    [SerializeField] private float jumpTime;
    public KeyCode jumpButton;
    private bool isGrounded;
    private bool isJumping;
    private int extraJumps;
    private float jumpTimeCounter;
    #endregion

    #region Shooting
    [Header("Shooting")]
    [SerializeField] Transform kunaiShootingPoint;
    [SerializeField] GameObject kunaiPrefab;
    private float fireRate = 0.5f;
    private float nextFire = 0f;
    #endregion

    #region Others
    [HideInInspector] public Vector2 LastPos;
    private Rigidbody2D myRB;
    private Animator myAnim;
    #endregion

    private void Start()
    {
        LastPos = transform.position;
        extraJumps = extraJumpsValue;
        myRB = GetComponent<Rigidbody2D>();
        jumpButton = KeyCode.Space;
        //myAnim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //if (Input.GetAxis("Fire1") > 0)
        //{
        //    ShootKunai();
        //}
    }

    private void FixedUpdate()
    {
        if (!DialogueManager.instance.inDialogue)
        {
            Move();
            Jump();

            if (myRB.velocity.y < 0)
            {
                myRB.gravityScale = 4f;
            }
        }
    }

    private void Move()
    {
        float move = Input.GetAxisRaw("Horizontal");

        //myAnim.SetFloat("speed", Mathf.Abs(move));

        myRB.velocity = new Vector2(move * speed * Time.deltaTime, myRB.velocity.y);

        DashMove(move);

        if (facingRight == false && move > 0)
        {
            Flip();
        }
        else if (facingRight == true && move < 0)
        {
            Flip();
        }
    }

    private void DashMove(float moveH)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashTime <= 0)
            {
                moveH = 0;
                dashTime = startDashTime;
                myRB.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;

                myRB.velocity = Vector2.right * moveH * dashSpeed;
            }
        }
        //transform.position = new Vector2(transform.position.x + (dashSpeed * moveH), transform.position.y);
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            isJumping = true;
            extraJumps = extraJumpsValue;
            myRB.gravityScale = 1f;
            jumpTimeCounter = jumpTime;
        }

        if (Input.GetKeyDown(jumpButton) && extraJumps > 0)
        {
            isGrounded = false;
            myRB.velocity = new Vector2(myRB.velocity.x, jumpPower);
            extraJumps--;
        }
        if (Input.GetKey(jumpButton) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, jumpPower);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(jumpButton))
        {
            isJumping = false;
        }

        //myAnim.SetBool("isGrounded", isGrounded);
        //myAnim.SetFloat("verticalSpeed", myRB.velocity.y);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private void ShootKunai()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (facingRight)
            {
                Instantiate(kunaiPrefab, kunaiShootingPoint.position, kunaiPrefab.transform.rotation);
            }
            else
            {
                Instantiate(kunaiPrefab, kunaiShootingPoint.position, Quaternion.Euler(new Vector3(0, 0, 90f)));
            }
        }
    }

    public void Die()
    {
        playerDied.Invoke();
        transform.position = LastPos;
    }
}
