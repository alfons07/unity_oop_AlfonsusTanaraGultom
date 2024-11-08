using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed = new Vector2(7f, 5f);
    [SerializeField] private Vector2 timeToFullSpeed = new Vector2(1f, 1f);
    [SerializeField] private Vector2 timeToStop = new Vector2(0.5f, 0.5f);
    [SerializeField] private Vector2 stopClamp = new Vector2(2.5f, 2.5f);

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;
    private Camera mainCamera;

    private void Start()
    {
        // Get RigidBody2D component
        rb = GetComponent<Rigidbody2D>();
        
        // Get main camera reference
        mainCamera = Camera.main;

        // Initial calculations for velocities and frictions
        CalculateInitialValues();
    }

    private void CalculateInitialValues()
    {
        // Calculate initial moveVelocity
        moveVelocity = new Vector2(
            2f * maxSpeed.x / timeToFullSpeed.x,
            2f * maxSpeed.y / timeToFullSpeed.y
        );

        // Calculate moveFriction
        moveFriction = new Vector2(
            -2f * maxSpeed.x / (timeToFullSpeed.x * timeToFullSpeed.x),
            -2f * maxSpeed.y / (timeToFullSpeed.y * timeToFullSpeed.y)
        );

        // Calculate stopFriction
        stopFriction = new Vector2(
            -2f * maxSpeed.x / (timeToStop.x * timeToStop.x),
            -2f * maxSpeed.y / (timeToStop.y * timeToStop.y)
        );
    }

    public void Move()
    {
        // Get input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        Vector2 currentVelocity = rb.velocity;
        Vector2 friction = GetFriction();

        // Handle horizontal movement
        if (moveDirection.x != 0)
        {
            float targetSpeedX = moveDirection.x * maxSpeed.x;
            currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, targetSpeedX, 
                Mathf.Abs(friction.x) * Time.fixedDeltaTime);
        }
        else if (Mathf.Abs(currentVelocity.x) > stopClamp.x)
        {
            float friction_x = currentVelocity.x > 0 ? stopFriction.x : -stopFriction.x;
            currentVelocity.x += friction_x * Time.fixedDeltaTime;
        }
        else
        {
            currentVelocity.x = 0;
        }

        // Handle vertical movement
        if (moveDirection.y != 0)
        {
            float targetSpeedY = moveDirection.y * maxSpeed.y;
            currentVelocity.y = Mathf.MoveTowards(currentVelocity.y, targetSpeedY, 
                Mathf.Abs(friction.y) * Time.fixedDeltaTime);
        }
        else if (Mathf.Abs(currentVelocity.y) > stopClamp.y)
        {
            float friction_y = currentVelocity.y > 0 ? stopFriction.y : -stopFriction.y;
            currentVelocity.y += friction_y * Time.fixedDeltaTime;
        }
        else
        {
            currentVelocity.y = 0;
        }

        // Apply movement
        rb.velocity = currentVelocity;

        // Apply boundary constraint
        MoveBound();
    }

    public Vector2 GetFriction()
    {
        return moveDirection != Vector2.zero ? moveFriction : stopFriction;
    }

    public void MoveBound()
    {
        // Get the screen bounds in world units
        Vector3 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        
        // Get the player position
        Vector3 playerPosition = transform.position;

        // Clamp the player's position to stay within the camera bounds
        playerPosition.x = Mathf.Clamp(playerPosition.x, -screenBounds.x, screenBounds.x);
        playerPosition.y = Mathf.Clamp(playerPosition.y, -screenBounds.y, screenBounds.y);

        // Update the player position
        transform.position = playerPosition;
    }

    public bool IsMoving()
    {
        return rb.velocity.sqrMagnitude > 0.1f;
    }
}

