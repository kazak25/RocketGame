using System;
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
        if (Input.touchSupported)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
                {
                    isJumping = true;
                    rb.velocity = Vector2.up * _jumpForce;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    isJumping = false;
                    horizontalMovement = 0f;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    horizontalMovement = touch.deltaPosition.x * Time.deltaTime;
                    horizontalMovement = Mathf.Clamp(horizontalMovement, -1f, 1f);
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isJumping = true;
                rb.velocity = Vector2.up * _jumpForce;
                horizontalMovement = Input.GetAxis("Horizontal");
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
            Vector2 movement = new Vector2(horizontalMovement * _horizontalMoveSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnRocketCrashed?.Invoke();
    }
}