using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ParootMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    readonly private float speed = 3;
    private float walk;
    private readonly float jumpStrength = 4.5f;
    public Collider2D Collider2D;
    private bool walkOverride;
    private bool dashCooldown;

    //gravity, walk, jump, dash
    private int disabledMovement = 0b0000;
    private float facingDir = 1;
    private IEnumerator OverrideWalking(float overrideTime)
    {
        walkOverride = true;
        yield return new WaitForSeconds(overrideTime);
        yield return new WaitUntil(() => (walk != 0));
        walkOverride = false;
    }
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void Walk(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            facingDir = callbackContext.ReadValue<float>();
        }
        walk = callbackContext.ReadValue<float>() * speed;
    }
    private void Update()
    {
        if ((disabledMovement & 0b100) != 0b100)
        {
            if (!walkOverride)
            {
                rb2d.velocity = new(walk, rb2d.velocity.y);
            }
        }
        GravityManager();

    }

    private void GravityManager()
    {
        if ((disabledMovement & 1000) == 1000)
        {
            rb2d.gravityScale = 0;
        }
        else
        {
            rb2d.gravityScale = 0.3f;
        }
    }
    public void Jump(CallbackContext callbackContext)
    {
        if ((disabledMovement & 0b010) != 0b010)
        {
            if (callbackContext.performed)
            {
                if (rb2d != null)
                {
                    if (Physics2D.Raycast(
                        Collider2D.bounds.center,
                        Vector2.down,
                        Collider2D.bounds.extents.y + 0.05f))
                    {
                        rb2d.velocity = new(rb2d.velocity.x, jumpStrength);
                    }
                    else if (Physics2D.Raycast(
                        Collider2D.bounds.center,
                        Vector2.right,
                        Collider2D.bounds.extents.x + 0.05f))
                    {
                        facingDir = -1;
                        float alteredJump;
                        alteredJump = Mathf.Sqrt((jumpStrength * jumpStrength) - (speed * speed));
                        StartCoroutine(OverrideWalking(0.2f));
                        rb2d.velocity = new(-speed, alteredJump);

                    }
                    else if (Physics2D.Raycast(
                        Collider2D.bounds.center,
                        Vector2.left,
                        Collider2D.bounds.extents.x + 0.05f))
                    {
                        facingDir = 1;
                        float alteredJump;
                        alteredJump = Mathf.Sqrt((jumpStrength * jumpStrength) - (speed * speed));
                        StartCoroutine(OverrideWalking(0.2f));
                        rb2d.velocity = new(speed, alteredJump);
                    }
                }
            }
        }
    }
    public void Dash(CallbackContext callbackContext)
    {
        if ((disabledMovement & 0b001) != 0b001)
        {
            if (callbackContext.performed)
            {
                StartCoroutine(DisableForTime(0.15f, 0b1110));
                rb2d.velocity = new(facingDir * speed * 3.3f, 0);
                StartCoroutine(DisableForTime(2, 0b1));
            }
        }
    }

    private IEnumerator DisableForTime(float time, int disabled)
    {
        disabledMovement |= disabled;
        yield return new WaitForSeconds(time);
        disabledMovement &= ~disabled;
    }
}
