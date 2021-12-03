using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;
    [SerializeField] Vector2[] colliderPoints;
    [SerializeField] public GameObject puf;
    private EdgeCollider2D thisCollider;
    [SerializeField] AudioManager audioManager;
    [SerializeField] RipplePostProcessor ripplePostProcessor;
    [SerializeField] float reactivationTime = 3;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject particle;
    [SerializeField] MenuManager menuManager;

    bool waiting = false;
    public bool lineWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<EdgeCollider2D>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        colliderPoints[0] = point1.transform.position + (point2.transform.position - point1.transform.position).normalized/2.5f;
        colliderPoints[1] = point2.transform.position + (point1.transform.position - point2.transform.position).normalized/2.5f;
        thisCollider.points = colliderPoints;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lineWaiting == false)
        {
            if (collision.gameObject.tag == "Rocket1")
            {
                //Debug.Log("ROCKEKETKETKEKT");
                float rippleAmount = Random.Range(15, 20);
                float frictionAmount = Random.Range(0.7f, 0.8f);
                ripplePostProcessor.Ripple(20, 0.8f, collision.gameObject.transform.position);
                audioManager.PlayBzzt();
                Stop(0.03f);
                Instantiate(puf,collision.gameObject.transform.position, Quaternion.identity);
                GameObject Effect = Instantiate(particle, collision.transform.position, collision.transform.rotation);
                collision.gameObject.GetComponent<Warning>().destroyWarning();
                Destroy(collision.gameObject);
            }

            else if (collision.gameObject.tag == "Laser")
            {
                StartCoroutine(ReactivateLine());
                lineRenderer.enabled = false;

            }
            else if(collision.gameObject.tag == "smallRocket")
            {
                float rippleAmount = Random.Range(20, 25);
                float frictionAmount = Random.Range(0.8f, 0.9f);
                ripplePostProcessor.Ripple(25, 0.85f, collision.gameObject.transform.position);
                audioManager.PlayBzzt();
                Stop(0.03f);
                Instantiate(puf, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "BigRocket")
            {
                audioManager.PlayBzzt();
            }
        }
    }

    public void destroyBigRocket(Collider2D collision)
    {
        float rippleAmount = Random.Range(20, 25);
        float frictionAmount = Random.Range(0.7f, 0.8f);
        ripplePostProcessor.Ripple(rippleAmount, frictionAmount, collision.gameObject.transform.position);
        audioManager.PlayBzzt();
        Stop(0.03f);
        Instantiate(puf, collision.gameObject.transform.position, Quaternion.identity);
        collision.gameObject.GetComponent<Warning>().destroyWarning();
        Destroy(collision.gameObject);
    }

    IEnumerator ReactivateLine()
    {
        lineWaiting = true;
        yield return new WaitForSecondsRealtime(reactivationTime);
        lineRenderer.enabled = true;
        lineWaiting = false;
    }

    void Stop(float duration)
    {
        if (waiting)
        {
            return;
        }

        Time.timeScale = 0;
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        if (menuManager.isPaused == false)
        {
            Time.timeScale = 1;
            waiting = false;
        }
    }
}
