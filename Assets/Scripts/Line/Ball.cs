using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    private Vector3 direction = Vector3.down;
    private readonly float speed = 6;
    private readonly int firstPointIndex = 0;

    private void Update() { Processing(); }

    private void Processing ()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void OnCollisionEnter (Collision other)
    {
        direction = Vector3.Reflect(direction, other.contacts[firstPointIndex].normal);
    }
}
