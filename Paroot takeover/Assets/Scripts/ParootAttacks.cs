using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ParootAttacks : MonoBehaviour
{
    public LayerMask enemyMask;
    public GameObject windBullet;
    public GameObject windTornado;
    private int shotsFired;
    public bool isLargeBulletReady;
    private IEnumerator LargeBulletCoroutine;
    private bool useBullets;

    //Wind Tornado, Wind bullets, Explosion
    private int disabledAttacks = 0b000;
    public void ExplosionReceiver(CallbackContext callbackContext)
    {
        if ((disabledAttacks & 0b1) != 0b1)
        {
            if (callbackContext.performed)
            {
                print("M1");
                StartCoroutine(Explosions());
                StartCoroutine(DisableForTime(1, 0b1));
            }
        }
    }
    public void TornadoReceiver(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if ((disabledAttacks & 0b100) != 0b100)
            {
                if (ParootMovement.directionMod > 0)
                {
                    Instantiate(windTornado, transform.position
                        + 1.18f
                        * ParootMovement.facingDir
                        * Vector3.right
                        + Vector3.up * 0.898748f, windTornado.transform.rotation);
                    StartCoroutine(DisableForTime(7, 0b100));

                    return;
                }
            }
        }
    }

    private IEnumerator LargeBullet()
    {
        yield return new WaitForSeconds(1.5f);
        isLargeBulletReady = true;

    }
    public void WindBullets(CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            useBullets = ParootMovement.directionMod == 0;
        }
        if (useBullets)
        {

            if ((disabledAttacks & 0b10) != 0b10)
            {
                if (callbackContext.started)
                {

                    LargeBulletCoroutine = LargeBullet();
                    StartCoroutine(LargeBulletCoroutine);
                }
                if (callbackContext.canceled)
                {
                    if (isLargeBulletReady)
                    {
                        GameObject currentBullet;
                        currentBullet = Instantiate(
                            windBullet,
                            new Vector2(transform.position.x + 1.2f * ParootMovement.facingDir, transform.position.y),
                            Quaternion.Euler(0, 0, 0));
                        currentBullet.transform.localScale = new Vector3(1.42f, 0.75f, 1.3f);
                        currentBullet.GetComponent<Rigidbody2D>().velocity = 5 * ParootMovement.facingDir * currentBullet.transform.right;
                        StartCoroutine(DisableForTime(3, 0b10));
                        isLargeBulletReady = false;
                        currentBullet.GetComponent<DestructableAttack>().damage = 10;
                        return;
                    }
                    else
                    {
                        StopCoroutine(LargeBulletCoroutine);
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        GameObject currentBullet;
                        currentBullet = Instantiate(
                            windBullet,
                            new Vector2(transform.position.x + 0.3f * ParootMovement.facingDir, transform.position.y),
                            Quaternion.Euler(0, 0, -30 + i * 20));
                        currentBullet.GetComponent<Rigidbody2D>().velocity = 5 * ParootMovement.facingDir * currentBullet.transform.right;
                    }
                    shotsFired++;
                    if (shotsFired == 3)
                    {
                        StartCoroutine(DisableForTime(1, 0b10));
                        shotsFired = 0;
                    }
                }
            }
        }
    }
    private IEnumerator Explosions()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(
            new(transform.position.x + 0.971f * ParootMovement.facingDir, transform.position.y),
            0.4082f, enemyMask);
        ApplyDamage(col, 4);
        yield return new WaitForSeconds(0.2f);
        col = Physics2D.OverlapCircleAll(
            new(transform.position.x + 1.1836f * ParootMovement.facingDir, transform.position.y - 0.1074f),
            0.1268081f, enemyMask);
        ApplyDamage(col, 1);
        yield return new WaitForSeconds(0.05f);
        col = Physics2D.OverlapCircleAll(
            new(transform.position.x + 0.835000038f * ParootMovement.facingDir, transform.position.y - 3.52609992f + 3.68f),
            0.1268081f, enemyMask);
        ApplyDamage(col, 1);
        yield return new WaitForSeconds(0.05f);
        col = Physics2D.OverlapCircleAll(
            new(transform.position.x + 0.815000057f * ParootMovement.facingDir, transform.position.y - 4.02759981f + 3.68f),
            0.1268081f, enemyMask);
        ApplyDamage(col, 1);
        yield return new WaitForSeconds(0.05f);
        col = Physics2D.OverlapCircleAll(
            new(transform.position.x + 1.1911f * ParootMovement.facingDir, transform.position.y - 3.3375001f + 3.68f),
            0.1268081f, enemyMask);
        ApplyDamage(col, 1);
    }

    private void ApplyDamage(Collider2D[] hits, float damage)
    {

        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<Health>().health-=damage;
        }
    }
    private void OnDrawGizmos()
    {
        /* Explosion Attack
        Gizmos.DrawWireSphere(new(transform.position.x + 0.971f * ParootMovement.facingDir, transform.position.y), 0.4082f);
        Gizmos.DrawWireSphere(new(transform.position.x + 1.1836f * ParootMovement.facingDir, transform.position.y - 0.1074f),
            0.1268081f);
        Gizmos.DrawWireSphere(new(transform.position.x + 0.835000038f * ParootMovement.facingDir, transform.position.y - 3.52609992f + 3.68f),
            0.1268081f);
        Gizmos.DrawWireSphere(new(transform.position.x + 0.815000057f * ParootMovement.facingDir, transform.position.y - 4.02759981f + 3.68f),
            0.1268081f);
        Gizmos.DrawWireSphere(new(transform.position.x + 1.1911f * ParootMovement.facingDir, transform.position.y - 3.3375001f + 3.68f),
            0.1268081f);
      */
    }
    private void Start()
    {
        LargeBulletCoroutine = LargeBullet();
    }
    private IEnumerator DisableForTime(float time, int disabled)
    {
        disabledAttacks |= disabled;
        yield return new WaitForSeconds(time);
        disabledAttacks &= ~disabled;
    }
}
