using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAfterTime : MonoBehaviour
{
    [SerializeField] float destroyTime = 1f;
    private Warning warningScript;
    private bool rocketInArena = true;
    [SerializeField] bool timerActive = false;
    [SerializeField] float timerDelay = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
        timerActive = false;
        warningScript = GetComponent<Warning>();
        
        StartCoroutine(startTimer());
    }

    private void FixedUpdate()
    {
        if (timerActive)
        {
            
            if (warningScript!=null && gameObject != null && !warningScript.arenaBorder.bounds.Contains(gameObject.transform.position))
            {
                if (gameObject.tag == "smallRocket" && gameObject.GetComponent<HomingMissile>().get_targetLocked())
                {
                    rocketInArena = false;
                    Destroy(gameObject, destroyTime);
                }
                else if(gameObject.tag != "smallRocket")
                {
                    rocketInArena = false;
                    Destroy(gameObject, destroyTime);
                }
            }
            else if(gameObject.tag != "smallRocket" && warningScript == null || gameObject == null)
            {
                rocketInArena = false;
                Destroy(gameObject, destroyTime);
            }
        }
    }

    IEnumerator destroy(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
    IEnumerator startTimer()
    {
        yield return new WaitForSeconds(timerDelay);
        timerActive = true;
    }
}
