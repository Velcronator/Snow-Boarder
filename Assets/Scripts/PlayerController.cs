using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueAmount = 3.0f;
    [SerializeField] private float groundCheckDistance = 2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float boostSpeed = 10f;

    Rigidbody2D rd2d;
    private SurfaceEffector2D surfaceEffector;
    private float speed;

    private void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        surfaceEffector = FindObjectOfType<SurfaceEffector2D>();
        speed = surfaceEffector.speed;
    }

    private void Update()
    {
        Rotate();
        RespondToBoost();
        RespondToJump();
    }

    private void RespondToJump()
    {
        // if the player presses the space key, apply a force upwards but only if grounded
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rd2d.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }

    private void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // increase the speed of the player
            surfaceEffector.speed = speed + boostSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // decrease the speed of the player
            surfaceEffector.speed = speed - boostSpeed;
        }
        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            surfaceEffector.speed = speed;
        }
    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // rotate the player to the left using torque
            rd2d.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // rotate the player to the right using torque
            rd2d.AddTorque(-torqueAmount);
        }
    }

    private bool IsGrounded()
    {
        // Cast a ray downwards to check for ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        // If the ray hits something, consider the player grounded
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
}
