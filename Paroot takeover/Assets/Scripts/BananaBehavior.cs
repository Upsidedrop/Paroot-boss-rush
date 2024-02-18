using UnityEngine;

public class BananaBehavior : MonoBehaviour
{
    private bool startTimer;
    private float timer = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(collision.gameObject.GetComponent<ParootMovement>().DisableForTime(2.5f, 0b0111));
            StartCoroutine(collision.gameObject.GetComponent<ParootAttacks>().DisableForTime(2.5f, 0b111));
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            startTimer = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Stun");
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            startTimer = true;
        }
        if (collision.gameObject.GetComponent<BananaBehavior>() != null)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {

        if (startTimer)
        {
            timer += Time.deltaTime;
        }
        if (timer > 3)
        {
            Destroy(gameObject);
        }
    }
}
