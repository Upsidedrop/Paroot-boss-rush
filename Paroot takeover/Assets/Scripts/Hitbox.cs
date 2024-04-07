using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int enemyLayer;
    public float damage;
    public delegate void Strike();
    public event Strike StrikeDelegate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            collision.GetComponent<Health>().health -= damage;
            StrikeDelegate.Invoke();
        }
    }
}
