using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ParootMovement : MonoBehaviour
{
    public LayerMask jumpableLayers;
    public LayerMask hazardsAndEnemies;
    private Rigidbody2D rb2d;
    readonly private float speed = 3;
    private float walk;
    private readonly float jumpStrength = 4.5f;
    public Collider2D Collider2D;
    private bool doubleJump;
    public static float directionMod;

    //gravity, walk, jump, dash
    [SerializeField]
    private int disabledMovement = 0b0000;
    public static float facingDir = 1;
    private bool iFramesActive = false;
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
            if (callbackContext.ReadValue<float>() > 0)
            {
                facingDir = 1;
            }
            else
            {
                facingDir = -1;
            }
        }
        walk = (float)callbackContext.ReadValue<float>() switch
        {
            > 0 => speed,
            < 0 => -speed,
            _ => 0,
        };
    }
    private void Update()
    {
        if ((disabledMovement & 0b100) != 0b100)
        {
            rb2d.linearVelocity = new(walk, rb2d.linearVelocity.y);
        }
        GravityManager();

    }
    private void GravityManager()
    {
        if ((disabledMovement & 0b1000) == 0b1000)
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
                        Collider2D.bounds.extents.y + 0.05f, jumpableLayers))
                    {
                        rb2d.linearVelocity = new(rb2d.linearVelocity.x, jumpStrength);
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
                        StartCoroutine(DisableForTime(0.2f, 0b100));
                        rb2d.linearVelocity = new(-speed, alteredJump);
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
                        StartCoroutine(DisableForTime(0.2f, 0b100));
                        rb2d.linearVelocity = new(speed, alteredJump);
                        doubleJump = true;
                        return;
                    }
                    if (doubleJump)
                    {
                        rb2d.linearVelocity = new(rb2d.linearVelocity.x, jumpStrength);
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


                StartCoroutine(IFrames(0.23f));


                if (directionMod < 0)
                {
                    rb2d.linearVelocity = new(0, -speed * 3.3f);
                }
                else
                {
                    rb2d.linearVelocity = new(facingDir * speed * 3.3f, 0);
                }
                StartCoroutine(DisableForTime(1, 0b1));
            }
        }
    }
    public IEnumerator DisableForTime(float time, int disabled)
    {
        disabledMovement |= disabled;
        yield return new WaitForSeconds(time);
        disabledMovement &= ~disabled;
    }
    public void CallCoroutine(string name, float param)
    {
        StartCoroutine(name, param);
    }
    public IEnumerator IFrames(float time)
    {

        if (iFramesActive)
        {
            yield break;
        }
        iFramesActive = true;
        Debug.Log("Setting collision mask for layer 7 to ignore hazards and enemies");
        Physics2D.SetLayerCollisionMask(7, ~hazardsAndEnemies);
        yield return new WaitForSeconds(time);
        print("Resetting collision mask for layer 7");
        Physics2D.SetLayerCollisionMask(7, 0b111111111);

        iFramesActive = false;

    }
}
