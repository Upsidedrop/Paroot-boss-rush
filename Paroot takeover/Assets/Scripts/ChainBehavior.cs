using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChainBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x < -10
            || transform.position.x > 10
            || transform.position.y > 8
            || transform.position.y < -8)
        {
            Destroy(gameObject);
        }
        Instantiate(gameObject, transform.position + transform.right * -0.67f, transform.rotation);
        StartCoroutine(Lifetime());
    }
    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
