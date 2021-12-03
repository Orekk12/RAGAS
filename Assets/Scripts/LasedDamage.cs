using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasedDamage : MonoBehaviour
{
    [SerializeField] Laser laser;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            laser.LaserDamage();
        }
    }
}
