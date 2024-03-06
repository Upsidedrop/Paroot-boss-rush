using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave_Behavior : MonoBehaviour
{
    public bool isMovingLeft = true;
    readonly float spikyness = 2;
    bool peaked = false;
    readonly float peakHeight =2;
    [SerializeField]
    GameObject ShockwaveBaby;
    private void Start()
    {
        StartCoroutine(SpawnDelay());
    }
    private void Update()
    {
        UpdateWave();
    }
    void UpdateWave()
    {
        if (transform.localScale.y > peakHeight)
        {
            peaked = true;
        }
        else if (transform.localScale.y < 0 || Mathf.Abs(transform.position.x) > 10)
        {
            Destroy(gameObject);
        }
        if (peaked)
        {
            transform.localScale += spikyness * Time.deltaTime * Vector3.down;
            transform.position += (spikyness / 2) * Time.deltaTime * Vector3.down;
        }
        else
        {
            transform.localScale -= spikyness * Time.deltaTime * Vector3.down;
            transform.position -= (spikyness / 2) * Time.deltaTime * Vector3.down;
        }

    }
    IEnumerator SpawnDelay()
    {
        GameObject temp;
        yield return new WaitForSeconds(0.1f);
        temp = Instantiate(ShockwaveBaby);
        temp.GetComponent<Shockwave_Behavior>().isMovingLeft = isMovingLeft;
        if (isMovingLeft)
        {
            temp.transform.position += Vector3.left * transform.localScale.x;
        }
        else
        {
            temp.transform.position += Vector3.right * transform.localScale.x;
        }

    }
}
