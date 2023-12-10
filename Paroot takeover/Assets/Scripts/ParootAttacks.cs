using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class ParootAttacks : MonoBehaviour
{
    public LayerMask enemyMask;
    public void ExplosionReceiver(CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            print("M1");
            StartCoroutine(Explosions());
        }
    }

    private IEnumerator Explosions()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(
            new(transform.position.x + 0.971f * ParootMovement.facingDir, transform.position.y),
            0.4082f,enemyMask);
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
            print($"Dealt {damage} damage to {hit.transform.name}");
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
}
