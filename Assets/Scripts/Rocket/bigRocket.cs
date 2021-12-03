using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigRocket : MonoBehaviour
{
    [Header("- - - References - - -")]
    [SerializeField] GameObject smallRocketPrefab;
    [SerializeField] RipplePostProcessor ripplePostProcessor;
    [SerializeField] Animator screenShaker;
    [SerializeField] AudioManager audioManager;

    [Header("- - - Variables - - -")]
    [SerializeField] float speed = 3f;
    [SerializeField] float splitAfterTime = 0.5f;
    [SerializeField] float spreadAngle = 360f;
    [SerializeField] float vibrateSpeed = 1f;
    [SerializeField] float vibrateAmount = 1f;
    public GameObject smallRocketTarget;
    public float period;
    [SerializeField] float bigRocketDestroyTime = 2f;
    [SerializeField] float bigRocketContactTime;
    private Rigidbody2D rb;
    [SerializeField] int smallRocketNum = 6;
    [SerializeField] float nextActionTime = 0f;
    [SerializeField] float time1;
    private float defSpeed;
    private bool onHit;


    // Start is called before the first frame update
    void Start()
    {
        onHit = true;
        defSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        //InvokeRepeating("StartCoroutine(splitTimer())", 0.2f, 2);

        ripplePostProcessor = FindObjectOfType<Camera>().GetComponent<RipplePostProcessor>();
        screenShaker = FindObjectOfType<Camera>().GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    bool started = false;
    void Update()
    {

        rb.velocity = transform.up * speed;
        /*if (gameObject.GetComponent<Warning>().arenaBorder.bounds.Contains(gameObject.transform.position) && !started)
        {
            StartCoroutine(splitTimer());
            started = true;
        }*/
        time1 = Time.time;
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;
           //nextActionTime += period;
            // execute block of code here
            if (gameObject.GetComponent<Warning>().arenaBorder.bounds.Contains(gameObject.transform.position))
            {
            
                shootMultipleOdd(smallRocketPrefab, smallRocketNum, gameObject.transform, spreadAngle);
            }
            //StartCoroutine(splitTimer());
        }
    }
    IEnumerator splitTimer()
    {
        if (gameObject.GetComponent<Warning>().arenaBorder.bounds.Contains(gameObject.transform.position))
        {
            yield return new WaitForSeconds(Random.Range(0.5f, splitAfterTime));
            shootMultipleOdd(smallRocketPrefab, smallRocketNum, gameObject.transform, spreadAngle);
        }
    }

    public void shootMultipleOdd(GameObject bullet, int bulletNum, Transform pivot, float spreadAngle)
    {
        float spreadOfEachBullet = spreadAngle / (bulletNum - 1);//25
        float spread = spreadAngle / 2;//50
        if (true)
        {
            for (int i = 0; i < bulletNum; i++)
            {
                if (true)
                {
                    GameObject Bullet = Instantiate(bullet, pivot.position, pivot.rotation);
                    Bullet.GetComponent<chooseTarget>().set_forceTarget(smallRocketTarget);
                    //Rigidbody2D rigB = Bullet.GetComponent<Rigidbody2D>();
                    Bullet.transform.Rotate(0, 0, spread);
                    //rigB.AddForce(Bullet.transform.right * bulletForce, ForceMode2D.Impulse);
                    spread -= spreadOfEachBullet;
                }
            }
        }
    }
    private Vector2 initalPos;
    [SerializeField] float bigRocketSlowAmount = 0.4f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Line" && bigRocketContactTime <= 0 && collision.gameObject.GetComponent<Line>().lineWaiting == false)
        {
            initalPos = transform.position;
            bigRocketContactTime = 0;
            speed *= bigRocketSlowAmount;
        }
    }

    [SerializeField] bool cycle=true;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Line" && collision.gameObject.GetComponent<Line>().lineWaiting == false)
        {

           
            bigRocketContactTime += Time.deltaTime;
            //transform.position = new Vector2(Mathf.Sin(Time.time * speed) * amount, transform.position.y);
            if(cycle)
            {
                cycle = false;
                transform.position = new Vector2(transform.position.x, transform.position.y + vibrateAmount);
            }
            else
            {
                cycle = true;
                transform.position = new Vector2(transform.position.x, transform.position.y - vibrateAmount);
            }
                

            if (bigRocketContactTime >= bigRocketDestroyTime)
            {
                //collision.gameObject.GetComponent<Line>().destroyBigRocket(collision);
                ripplePostProcessor.Ripple(40, 0.9f, gameObject.transform.position);
                screenShaker.SetTrigger("Shake1");
                audioManager.PlayBigExplosion();
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Line" && collision.gameObject.GetComponent<Line>().lineWaiting == false)
        {
            transform.position = new Vector2(transform.position.x, initalPos.y);
            speed = defSpeed;
        }
    }
  
}
