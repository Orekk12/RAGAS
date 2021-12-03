using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activecol : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D col;
    void Awake()
    {
        StartCoroutine(abab());
    }

    // Update is called once per frame
    private IEnumerator abab()
    {       
        yield return new WaitForSeconds(3.5f);
        col.enabled = true;
    }

    
}
