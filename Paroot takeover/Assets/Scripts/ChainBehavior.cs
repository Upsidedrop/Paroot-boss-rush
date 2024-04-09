using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(gameObject, transform.position + transform.right * -0.67f, transform.rotation);
        StartCoroutine(Lifetime());
    }
    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }
}
