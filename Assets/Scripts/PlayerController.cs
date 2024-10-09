using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody rb;

    private Vector3 moveInput;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float idleSlow = 0.9f;

    [HideInInspector] public Vector2 lastMoveDir;
    private bool isFacingRight = true;

    [Header("Sprite")]
    [SerializeField] SpriteRenderer spriteRenderer;

    void Start()
    {
        
    }

    
    void Update()
    {
        Move();
        Flip();
    }

    //-------------------------------------------------------------
    // MOVEMENT
    //-------------------------------------------------------------
    private void Move()
    {
        if (moveInput != Vector3.zero)
        {
            rb.velocity = moveInput * moveSpeed;
        }
        else
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, idleSlow);
        }
    }

    // ---------------------------------
    // SPRITE & ANIMATIONS
    // ---------------------------------
    private void Flip()
    {
        if (isFacingRight && moveInput.x < 0 || !isFacingRight && moveInput.x > 0)
        {
            isFacingRight = !isFacingRight;
        }

        spriteRenderer.flipX = isFacingRight;
    }

    //-------------------------------------------------------------
    // INPUT CHECKS
    //-------------------------------------------------------------
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }    
}
