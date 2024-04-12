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
    public GameObject m1Hitbox;
    private readonly float maxHealth = 100;

    //Heal, Wind Tornado, Wind bullets, Explosion
    private int disabledAttacks = 0b0000;
    public void ExplosionReceiver(CallbackContext callbackContext)
    {
        if ((disabledAttacks & 0b1) != 0b1)
        {
            if (callbackContext.performed)
            {
                if (ParootMovement.directionMod == 0)
                {
                    print("M1");
                    StartCoroutine(Explosions());
                    StartCoroutine(DisableForTime(3, 0b1));
                }

            }
        }
    }

    private IEnumerator Heal()
    {
        StartCoroutine(DisableForTime(0.75f, 0b110111));
        StartCoroutine(GetComponent<ParootMovement>().DisableForTime(0.75f, 0b0111));
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < maxHealth / 5; i++)
        {
            if (GetComponent<Health>().health >= maxHealth)
            {
                yield break;
            }
            GetComponent<Health>().health++;
            yield return new WaitForSeconds(0.35f);

        }
    }
    public void HealReceiver(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            if ((disabledAttacks & 0b1000) != 0b1000)
            {
                if (ParootMovement.directionMod < 0)
                {
                    print((disabledAttacks & 0b1000) != 0b1000);

                    StartCoroutine(DisableForTime(35, 0b1000));
                    StartCoroutine(Heal());
                }

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
        yield return new WaitForSeconds(1);
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
                        StartCoroutine(DisableForTime(5, 0b10));
                        isLargeBulletReady = false;
                        currentBullet.GetComponent<DestructableAttack>().damage = 15;
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
                        StartCoroutine(DisableForTime(2, 0b10));
                        shotsFired = 0;
                    }
                }
            }
        }
    }
    private IEnumerator Explosions()
    {
        Vector2 attackPos;
        attackPos = transform.position;
        float attackDir = ParootMovement.facingDir;
        GameObject col = Instantiate(m1Hitbox, new(attackPos.x + 0.971f * attackDir, attackPos.y), Quaternion.identity);
        col.transform.localScale = Vector3.one * 0.82f;
        col.GetComponent<Hitbox>().damage = 5;

        yield return new WaitForSeconds(0.3f);
        Destroy(col);
        col = Instantiate(m1Hitbox, new(attackPos.x + 1.1836f * attackDir, attackPos.y - 0.1074f), Quaternion.identity);

        yield return new WaitForSeconds(0.075f);
        Destroy(col);
        col = Instantiate(m1Hitbox, new(attackPos.x + 0.835000038f * attackDir, attackPos.y - 3.52609992f + 3.68f), Quaternion.identity);

        yield return new WaitForSeconds(0.075f);
        Destroy(col);
        col = Instantiate(m1Hitbox, new(attackPos.x + 0.815000057f * attackDir, attackPos.y - 4.02759981f + 3.68f), Quaternion.identity);

        yield return new WaitForSeconds(0.075f);
        Destroy(col);
        col = Instantiate(m1Hitbox, new(attackPos.x + 1.1911f * attackDir, attackPos.y - 3.3375001f + 3.68f), Quaternion.identity);
        yield return new WaitForSeconds(0.075f);
        Destroy(col);
    }
    private void Start()
    {
        LargeBulletCoroutine = LargeBullet();
    }
    public IEnumerator DisableForTime(float time, int disabled)
    {
        print($"Disabling {disabled}");
        disabledAttacks |= disabled;
        yield return new WaitForSeconds(time);
        disabledAttacks &= ~disabled;
    }
}
