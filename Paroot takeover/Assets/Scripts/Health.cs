using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;


    private void Update()
    {
        if (health <= 0)
        {

            Destroy(gameObject);

        }
    }
}
