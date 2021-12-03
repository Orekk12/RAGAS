using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard_Movement : MonoBehaviour
{
    [SerializeField] public float Speed = 2f;
    [SerializeField] Animator Char1Movement;
    //[SerializeField] Animator tempAnim1;
    Rigidbody2D rb;
    Vector2 direction;
    Vector3 mouseposition;
    float x, y;
    private float oldx, oldy;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        y = 0;
        x = 0;
        if (Input.GetKey(KeyCode.A)) { x += -1; }
        if (Input.GetKey(KeyCode.D)) { x +=  1; }
        if (Input.GetKey(KeyCode.W)) { y +=  1; }
        if (Input.GetKey(KeyCode.S)) { y += -1; }
        movementAnimation();
       
    }
    private void FixedUpdate()
    {
        direction = new Vector2(x, y);
        rb.velocity = direction.normalized * Speed;
    }

    void movementAnimation()
    {
        if (Char1Movement != null)
        {
            float animx = rb.velocity.x;
            float animy = rb.velocity.y;
            if (rb.velocity.x == 0 && rb.velocity.y == 0)
            {
                animx = oldx;
                animy = oldy;
            }
            if (!(rb.velocity.x == 0 && rb.velocity.y == 0))
            {
                oldx = rb.velocity.x;
                oldy = rb.velocity.y;
            }
            Char1Movement.SetFloat("x", animx);
            Char1Movement.SetFloat("y", animy);
            //tempAnim1.SetFloat("x", animx);
            //tempAnim1.SetFloat("y", animy);
        }
    }
}
