using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.05f;
    public float maxSpeed = 5f;
    public float accelerationTime = 1f;
    public float decelerationTime = 1f;
    public float apexHeight = 2f;
    public float apexHeightMax = 2f;
    public float apexHeightMin = 0.5f;
    public float apexTime = 0.4f;
    private float gravity = 0f;
    private float jumpVelocity = 7f;
    public float terminalSpeed = -2f;
    public float coyoteTime = 0.1f;
    private float coyoteTimer = 0f;
    public LayerMask ground;
    public float bounceForce = 8f;
    public float dash = 10f;
    public float dashCooldown = 1f;
    private float dashCooldownTimer = 0f;
    private bool isDashing = false;
    private float dashDuration = 0.2f;
    private float dashDurationTimer = 0f;
    private bool isHolding = false;
    private float jumpTime = 10f;

    public float walkingDeadzone = 0.05f;

    private Rigidbody2D rb;
    public BoxCollider2D BoxCollider;
    private Vector2 velocity = Vector2.zero;
    private FacingDirection currentDirection;



    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //BoxCollider = GetComponent<BoxCollider2D>();

       
    }

    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        float horizontal = Input.GetAxisRaw("Horizontal");
        float jump = Input.GetAxisRaw("Jump");
        Vector2 playerInput = new Vector2(horizontal, jump);
        Dashing();
        MovementUpdate(playerInput);

        //Debug.Log(IsGrounded());      

        //Debug.Log(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        
        if (isDashing)
        {
            return;
        }
        
        float acceleration = maxSpeed / accelerationTime;
        float deceleration = maxSpeed / decelerationTime;

        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimer > 0 && rb.linearVelocity.y <= 0)
        {
            isHolding = true;
            apexHeight = apexHeightMax;

            gravity = (-2 * apexHeight) / (apexTime * apexTime);
            jumpVelocity = (2 * apexHeight) / apexTime;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
            coyoteTimer = 0;
        }
        //if (Input.GetKey(KeyCode.Space) && isHolding)
        //{

        //apexHeight += Time.deltaTime * 5f;

        //Debug.Log("max height");
        //}


        if (Input.GetKeyUp(KeyCode.Space))
        {

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * (apexHeightMin / apexHeightMax));

            isHolding = false;


        }

        if (rb.linearVelocity.y > 0)
        {
            if (isHolding)
            {
                rb.linearVelocity += new Vector2(0, gravity * 0.6f * Time.deltaTime);

            }
            else
            {
                rb.linearVelocity += new Vector2(0, gravity * 2f * Time.deltaTime);
            }
        }
        else
        {
            rb.linearVelocity += new Vector2(0, gravity * Time.deltaTime);
        }

        if (playerInput.x != 0)
        {
            velocity += playerInput * acceleration * Time.deltaTime;
        }
        else
        {
            float reduceVelocity = velocity.magnitude - deceleration * Time.deltaTime;

            if (reduceVelocity < 0) reduceVelocity = 0;

            velocity = velocity.normalized * reduceVelocity;
        }

        if (IsGrounded())
        {
            if (coyoteTimer != coyoteTime) //reset timer if timer is not reset
            {
                coyoteTimer = coyoteTime;
            }
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
            //Debug.Log(coyoteTimer);
        }

       


        if (rb.linearVelocity.y < terminalSpeed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, terminalSpeed);
            }

            velocity = Vector2.ClampMagnitude(velocity, maxSpeed);

            rb.linearVelocity = new Vector2(velocity.x, rb.linearVelocity.y);
        }
    

    public bool IsWalking()
    {
        if (Mathf.Abs(velocity.x) > walkingDeadzone)
        {
            //Debug.Log("Walking is true");
            return true;
        }

        else
        {
            return false;
        }
    }
    public bool IsGrounded()
    {
        if (BoxCollider.IsTouchingLayers(ground))
        {
            //Debug.Log("true");
            return true;
        }
        else
        {
            //Debug.Log("false");
            return false;
        }
    }

    public FacingDirection GetFacingDirection()
    {
        if (velocity.x <= -1)
        {
            currentDirection = FacingDirection.left;
        }
        else if (velocity.x >= 1)
        {
            currentDirection = FacingDirection.right;
        }

        return currentDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pad"))
        {
            Debug.Log("triggered bounce pad");

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);

            coyoteTimer = 0f;
        }
    }
    private void Dashing()
    {
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
            Debug.Log(dashCooldownTimer);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0 && !isDashing)
        {
            Debug.Log("Dashing");
            
            isDashing = true;
            dashCooldownTimer = dashCooldown;
            dashDurationTimer = dashDuration;
            if (currentDirection == FacingDirection.right)
            {
                rb.linearVelocity = new Vector2(dash, 0);
            }
            else
            {
                rb.linearVelocity = new Vector2(-dash, 0);
            }
        }

        if (isDashing)
        {
            dashDurationTimer -= Time.deltaTime;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            if (dashDurationTimer <= 0f)
            {
                isDashing = false;
            }
            //return;
        }
        
    }

}

