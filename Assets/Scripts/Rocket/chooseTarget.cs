using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class chooseTarget : MonoBehaviour
{
    [Header("- - - References - - -")]
    [SerializeField] GameObject[] Targets;
    [SerializeField] GameObject currentTarget;
    [SerializeField] GameObject forceTarget;

    [Header("- - - Variables - - -")]
    [SerializeField] bool canChangeTarget = true;

    private float dist1;
    private float dist2;

    // Start is called before the first frame update
    void Start()
    {
        startRocket();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        startRocket();
    }

    void selectTarget()
    {
        //Debug.Log(Targets.Length);
        if (Targets.Length != 0)
        {
            dist1 = Vector3.Distance(gameObject.transform.position, Targets[0].transform.position);
            dist2 = Vector3.Distance(gameObject.transform.position, Targets[1].transform.position);
            if (dist1 > dist2)
            {
                currentTarget = Targets[1];
            }
            else
            {
                currentTarget = Targets[0];
            }
        }
    }

    void setTarget(Transform forceTarget, Transform currTarget)
    {
       
        if (forceTarget!= null && !canChangeTarget)
        {
            //gameObject.GetComponent<AIDestinationSetter>().target = forceTarget;
            gameObject.GetComponent<HomingMissile>().target = forceTarget;
        }
        else if (canChangeTarget)
        {
            //gameObject.GetComponent<AIDestinationSetter>().target = currTarget;
            gameObject.GetComponent<HomingMissile>().target = currTarget;

        }
            
    }
    public void setAllTargets(GameObject t1, GameObject t2)
    {
        Targets[0] = t1;
        Targets[1] = t2;
    }
    public void set_forceTarget(GameObject ftarget)
    {
        forceTarget = ftarget;
    }
    void startRocket()
    {
        selectTarget();
        if(currentTarget != null)
        {
            setTarget(forceTarget.transform, currentTarget.transform);
        }
        else
        {
            setForceTarget(forceTarget.transform);
        }
    }
    void setForceTarget(Transform forceTarget)
    {
        if (forceTarget != null && !canChangeTarget)
        {
            //gameObject.GetComponent<AIDestinationSetter>().target = forceTarget;
            gameObject.GetComponent<HomingMissile>().target = forceTarget;
        }
    }
}
