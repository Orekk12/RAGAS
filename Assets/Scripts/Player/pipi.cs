using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipi : MonoBehaviour
{

    [SerializeField] Transform pivot;
    [SerializeField] float speed;
    // Update is called once per frame
    void Update()
    {
        Vector2 direction = pivot.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        /*
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
        */
        //gameObject.transform.rotation = rotation;
        //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }
}
