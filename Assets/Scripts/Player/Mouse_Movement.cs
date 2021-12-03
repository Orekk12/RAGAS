using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Movement : MonoBehaviour

{
    [SerializeField] public float Speed=2f;
    [SerializeField] Animator Char2Movement;
    //[SerializeField] Animator tempAnim2;
    Rigidbody2D rb;
    Vector2 direction;
    Vector3 mouseposition;
    bool flag = true;
    bool flag2 = false;
    private float oldx, oldy;
    public GameObject Target;
    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(0, 6);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    // Update is called once per frame
    void Update()
    {
        //Vector3 mouseposition = Input.mousePosition;
        //mouseposition = Camera.main.ScreenToWorldPoint(mouseposition);
        //Debug.Log(flag);

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Player2")
            {
                Target = hit.collider.gameObject;
                Debug.Log("Hit");
                flag = false;
            }
            else
            {
                flag = true;
            }

        }

        if (flag)
        {
            Vector3 mouseposition = Input.mousePosition;
            mouseposition = Camera.main.ScreenToWorldPoint(mouseposition);
            Vector2 direction = new Vector2(mouseposition.x - rb.transform.position.x, mouseposition.y - rb.transform.position.y);
            rb.velocity = direction.normalized * Speed;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
        movementAnimation();
    }
    private void FixedUpdate()
    {
        ////Debug.Log(flag);
        //if (flag) 
        //{ 
        //    Vector3 mouseposition = Input.mousePosition;
        //    mouseposition = Camera.main.ScreenToWorldPoint(mouseposition);
        //    Vector2 direction = new Vector2(mouseposition.x - rb.transform.position.x, mouseposition.y - rb.transform.position.y);
        //    rb.velocity = direction.normalized * Speed;
        //}
        //else
        //{
        //    rb.velocity = new Vector2(0, 0);
        //}
        //movementAnimation();
    }
    void OnMouseEnter()
    {
        //Debug.Log("Enter");
        flag = false;

    }
    //private void OnMouseDown()
    //{
    //    flag = false;
    //    flag2 = true;
    //}
    void OnMouseExit()
    {
        //Debug.Log("Exit");
        //if (!flag2) { 
        //    flag = true;
        //}
        flag = true;
    }
    void movementAnimation()
    {
        if (Char2Movement != null)
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
            Char2Movement.SetFloat("x", animx);
            Char2Movement.SetFloat("y", animy);
            //tempAnim2.SetFloat("x", animx);
            //tempAnim2.SetFloat("y", animy);
        }
    }

}