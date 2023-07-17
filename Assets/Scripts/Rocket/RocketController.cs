using UnityEngine;
using UnityEngine.Events;

public class RocketController : MonoBehaviour
{
    public UnityEvent OnRocketCrashed;

    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _horizontalMoveSpeed = 5f;

    public Rigidbody2D rb;
    private bool isJumping = false;
    private float horizontalMovement = 0f;

    void Update()
    {
        horizontalMovement = 0f;
        if (Input.touchSupported)
        {
            if (Input.touchCount > 0)
            {
                Touch[] touches = Input.touches;

                foreach (Touch touch in touches)
                {
                    if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
                    {
                        isJumping = true;
                        rb.velocity = Vector2.up * _jumpForce;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        isJumping = false;
                        
                        //horizontalMovement = 0f;
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        horizontalMovement = touch.deltaPosition.normalized.x * _horizontalMoveSpeed;
                    }
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isJumping = true;
                rb.velocity = Vector2.up * _jumpForce;
                horizontalMovement = Input.GetAxis("Horizontal") * _horizontalMoveSpeed;
            }
            else
            {
                isJumping = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            Vector2 movement = new Vector2(horizontalMovement, rb.velocity.y);
            rb.velocity = movement;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnRocketCrashed?.Invoke();
    }
    
}