using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.05f;
    public float maxSpeed = 5f;
    public float accelerationTime = 1f;
    public float decelerationTime = 1f;
    public float jumpStength = 500f;
    public float jumpAcceleration = 1f;
    public LayerMask ground;

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
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        float horizontal = Input.GetAxisRaw("Horizontal");
        float jump = Input.GetAxisRaw("Jump");
        Vector2 playerInput = new Vector2(horizontal, jump);
        MovementUpdate(playerInput);
        Debug.Log(IsGrounded());      

        //Debug.Log(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        float acceleration = maxSpeed / accelerationTime;
        float deceleration = maxSpeed / decelerationTime;
   

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

        if (Input.GetAxisRaw("Jump") > 0 && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpStength);
        }
      
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);

        rb.linearVelocity = new Vector2(velocity.x, rb.linearVelocity.y);

    }

    public bool IsWalking()
    {
        if (Mathf.Abs(velocity.x) > walkingDeadzone)
        {
            Debug.Log("Walking is true");
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

        
}
