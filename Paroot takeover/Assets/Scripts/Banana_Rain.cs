using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana_Rain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnDelay()); 
    }

    IEnumerator SpawnDelay()
    {
        while(!(transform.position.y < 0f))
        {
            yield return null;
        }
        if (Random.Range(0,5) == 0)
        {
            yield break;
        }
        GameObject temp = Instantiate(gameObject, new Vector2(Random.Range(-8,8), 4), Quaternion.Euler(new(0,0,90)));
        temp.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * Random.Range(1.0f, 2.0f);
    }
}
