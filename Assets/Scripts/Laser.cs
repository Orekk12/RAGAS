using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Laser Settings")]
    [SerializeField] float fireDistance = 100;
    [SerializeField] GameObject[] targets;
    [SerializeField] float laserDissapearingSpeed = 0.8f;
    [SerializeField] float laserDamage = 250f;
    [SerializeField] float laserInterval = 10;


    [Header("---")]
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Texture laserSprite;
    [SerializeField] Texture indicator;
    [SerializeField] gameState gameState;
    [SerializeField] GameObject colliderObject;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] Animator shakeAnimator;
    [SerializeField] RipplePostProcessor ripplePostProcessor;
    [SerializeField] AudioManager audioManager;
    [SerializeField] MenuManager menuManager;

    int target;
    Vector2 firePos, targetPos;
    int direction;

    Vector3 firePosition;
    Vector3 targetPosition;

    bool waiting;
    bool isFired = true;

    void Awake()
    {
        lineRenderer.material.SetTexture("_MainTex", indicator);
    }

    void Start()
    {
        StartCoroutine(DontFire());
    }

    void Update()
    {   
        LaserUpdate();
    }

    IEnumerator DontFire()
    {
        yield return new WaitForSeconds(10);
        isFired = false;
    }

    void LaserUpdate()
    {
        if (isFired)
        {
            return;
        }

        StartCoroutine(LaserEverySecond(laserInterval));
    }

    IEnumerator LaserEverySecond(float waitTime)
    {
        isFired = true;
        FireLaser();
        yield return new WaitForSeconds(waitTime);
        if (laserInterval > 2)
        {
            laserInterval = laserInterval-(laserInterval/50);
        }
        isFired = false;
    }

    void ShootLaser(Vector3 firePosition, Vector3 aimedPosition)
    {
        lineRenderer.enabled = true; //enable the line renderer
        lineRenderer.SetPosition(0, firePosition); //shoot a line between the given points
        lineRenderer.SetPosition(1, aimedPosition);

        firePos = new Vector2(firePosition.x, firePosition.y);
        targetPos = new Vector2(aimedPosition.x, aimedPosition.y);

        Invoke("ChangeTextureLaser", laserDissapearingSpeed);
    }

    void ChangeTextureLaser()
    {
        Debug.Log("changed");
        lineRenderer.material.SetTexture("_MainTex", laserSprite);
        DrawCollider();
        audioManager.PlayLaserBeam();
        shakeAnimator.SetTrigger("Shake1");
        float rippleAmount = Random.Range(25, 50);
        float frictionAmount = Random.Range(0.9f, 0.95f);
        Stop(0.07f);
        ripplePostProcessor.Ripple(rippleAmount, frictionAmount, lineRenderer.transform.position);



        Invoke("DisableLineRenderer", laserDissapearingSpeed); //disable line renderer after given time
    }

    void DrawCollider()
    {
        if (direction == 0)
        {
            DrawColliderVertical();
        }

        else
        {
            DrawColliderHorizontal();
        }
    }

    void DisableLineRenderer()
    {
        boxCollider.enabled = false;
        lineRenderer.enabled = false; //disable line renderer
        lineRenderer.material.SetTexture("_MainTex", indicator);
    }

    void FireLaser()
    {
        direction = Random.Range(0, 2);
        target = Random.Range(0, 2);

        Vector3 playerTransform = targets[target].transform.position;
        
        if (direction == 0)
        {
            firePosition = new Vector3(playerTransform.x, playerTransform.y+fireDistance, playerTransform.z);
            targetPosition = new Vector3(playerTransform.x, playerTransform.y-fireDistance, playerTransform.z);
        }

        else if (direction == 1)
        {
            firePosition = new Vector3(playerTransform.x+fireDistance, playerTransform.y, playerTransform.z);
            targetPosition = new Vector3(playerTransform.x-fireDistance, playerTransform.y, playerTransform.z);
        }

        ShootLaser(firePosition, targetPosition);
    }

    void DrawColliderHorizontal()
    {
        boxCollider.enabled = true;
        boxCollider.size = new Vector2(fireDistance, 0.5f);
        colliderObject.transform.position = (firePos+targetPos)/2;
    }

    void DrawColliderVertical()
    {
        boxCollider.enabled = true;
        boxCollider.size = new Vector2(0.5f, fireDistance);
        colliderObject.transform.position = (firePos+targetPos)/2;
    }

    public void LaserDamage()
    {
        Debug.Log(targets[target].GetComponentInChildren<SpriteRenderer>());
        gameState.damage_player(laserDamage, targets[target].GetComponentInChildren<SpriteRenderer>(), targets[target].GetComponent<Collider2D>());
        Debug.Log("Laser Hit");
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
