using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDust : MonoBehaviour
{
    // Start is called before the first frame update
    public float alivetime = 0.3f;
    void Start()
    {
        Destroy(gameObject, alivetime);
    }
}
