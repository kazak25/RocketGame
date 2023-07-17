using UnityEngine;
using UnityEngine.Events;

public class RocketController : MonoBehaviour
{
    public UnityEvent OnRocketCrashed;

    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _horizontalMoveSpeed = 5f;
    [SerializeField] private Rigidbody2D _rb;

    private bool _isJumping = false;
    private float _horizontalMovement = 0f;

    void Update()
    {
        _horizontalMovement = 0f;
        if (Input.touchSupported)
        {
            if (Input.touchCount > 0)
            {
                Touch[] touches = Input.touches;

                foreach (Touch touch in touches)
                {
                    if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
                    {
                        _isJumping = true;
                        _rb.velocity = Vector2.up * _jumpForce;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        _isJumping = false;
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        _horizontalMovement = touch.deltaPosition.normalized.x * _horizontalMoveSpeed;
                    }
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _isJumping = true;
                _rb.velocity = Vector2.up * _jumpForce;
                _horizontalMovement = Input.GetAxis("Horizontal") * _horizontalMoveSpeed;
            }
            else
            {
                _isJumping = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (_isJumping)
        {
            Vector2 movement = new Vector2(_horizontalMovement, _rb.velocity.y);
            _rb.velocity = movement;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnRocketCrashed?.Invoke();
    }
}