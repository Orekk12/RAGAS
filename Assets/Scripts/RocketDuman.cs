using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDuman : MonoBehaviour
{
    public GameObject duman;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(smoke());
    }

    private IEnumerator smoke()
    {
        while (true)
        {
            GameObject dumanxd = Instantiate(duman, gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
