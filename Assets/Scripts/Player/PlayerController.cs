using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody rb;

    private Vector3 moveInput;
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float idleSlow = 0.9f;
    private float curSpeed;

    [HideInInspector] public Vector2 lastMoveDir;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1.0f;

    [Header("Shooting")]
    [SerializeField] private WeaponManager weaponManager;

    [Header("Sprite & Animation")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteRenderer pistolSprite;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Animator animator;

    private void Start()
    {
        weaponManager.Initialise(animator);
    }

    private void FixedUpdate()
    {
        if (isDashing) { return; }
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

        curSpeed = Mathf.Abs(new Vector2(rb.velocity.x, rb.velocity.z).magnitude);
        animator.SetFloat("Speed", curSpeed);
    }

    private IEnumerator Dash() // TODO: Make player unable to be damaged when dashing
    {
        canDash = false;
        isDashing = true;
        rb.velocity = moveInput * (dashDistance / dashDuration);
        // End of dash
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        // End of dash cooldown
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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

        //if (isFacingRight && aimPos.x < transform.position.x || !isFacingRight && aimPos.x > transform.position.x)
        //{
        //    isFacingRight = !isFacingRight;
        //}

        spriteRenderer.flipX = !isFacingRight;

        if (isFacingRight)
        {
            weaponHolder.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            weaponHolder.localScale = new Vector3(-1, 1, 1);
        }

        if (curSpeed > 0.01f)
        {
            pistolSprite.enabled = true;
        }
        else
        {
            pistolSprite.enabled = false;
        }
    }

    //-------------------------------------------------------------
    // INPUT CHECKS
    //-------------------------------------------------------------
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        //if (context.performed && !isDashing)
        //{
        //    weaponManager.Shoot();
        //}

        if (context.started)
        {
            weaponManager.StartShooting();
        }
        else if (context.canceled)
        {
            weaponManager.StopShooting();
        }
    }

    public void OnSwapWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weaponManager.SwapWeapon();
        }
    }
}
