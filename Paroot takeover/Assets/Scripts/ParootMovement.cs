using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ParootMovement : MonoBehaviour
{
    public LayerMask jumpableLayers;
    private Rigidbody2D rb2d;
    readonly private float speed = 3;
    private float walk;
    private readonly float jumpStrength = 4.5f;
    public Collider2D Collider2D;
    private bool doubleJump;
    public static float directionMod;

    //gravity, walk, jump, dash
    private int disabledMovement = 0b0000;
    public static float facingDir = 1;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void GetDirectional(CallbackContext callbackContext)
    {
        directionMod = callbackContext.ReadValue<float>();
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
            rb2d.velocity = new(walk, rb2d.velocity.y);
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
                        Collider2D.bounds.extents.y + 0.05f,jumpableLayers))
                    {
                        rb2d.velocity = new(rb2d.velocity.x, jumpStrength);
                        doubleJump = true;
                        return;
                    }
                    else if (Physics2D.Raycast(
                        Collider2D.bounds.center,
                        Vector2.right,
                        Collider2D.bounds.extents.x + 0.05f, jumpableLayers))
                    {
                        facingDir = -1;
                        float alteredJump;
                        alteredJump = Mathf.Sqrt((jumpStrength * jumpStrength) - (speed * speed));
                        StartCoroutine(DisableForTime(0.2f, 100));
                        rb2d.velocity = new(-speed, alteredJump);
                        doubleJump = true;
                        return;
                    }
                    else if (Physics2D.Raycast(
                        Collider2D.bounds.center,
                        Vector2.left,
                        Collider2D.bounds.extents.x + 0.05f, jumpableLayers))
                    {
                        facingDir = 1;
                        float alteredJump;
                        alteredJump = Mathf.Sqrt((jumpStrength * jumpStrength) - (speed * speed));
                        StartCoroutine(DisableForTime(0.2f, 100));
                        rb2d.velocity = new(speed, alteredJump);
                        doubleJump = true;
                        return;
                    }
                    if (doubleJump)
                    {
                        rb2d.velocity = new(rb2d.velocity.x, jumpStrength);
                        doubleJump = false;
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
                StartCoroutine(DisableForTime(0.23f, 0b1110));
                if (directionMod < 0)
                {
                    rb2d.velocity = new(0, -speed * 3.3f);
                }
                else
                {
                    rb2d.velocity = new(facingDir * speed * 3.3f, 0);
                }
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
