using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParootParry : MonoBehaviour
{
    public ParootAttacks parootAttacks;
    private void Start()
    {
        StartCoroutine(LifeTime());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DestructableAttack>() != null)
        {
            parootAttacks.heldItems.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
