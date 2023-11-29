using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ParootMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    readonly private float speed = 3;
    private float walk;
    private readonly float jumpHeight = 8;
    public Collider2D Collider2D;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void Walk(CallbackContext callbackContext)
    {

        walk = callbackContext.ReadValue<float>() * speed;
    }
    private void Update()
    {
        if (rb2d != null)
        {
            rb2d.velocity = new(walk, rb2d.velocity.y);
        }
    }
    public void Jump(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if (rb2d != null)
            {
                if (Physics2D.Raycast(
                    Collider2D.bounds.center,
                    Vector2.down,
                    Collider2D.bounds.extents.y + 0.3f))
                {

                    rb2d.velocity = new(rb2d.velocity.x, jumpHeight);
                }
            }
        }
    }
}
