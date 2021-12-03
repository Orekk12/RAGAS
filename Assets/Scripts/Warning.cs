using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField] GameObject warningPrefab;
    private GameObject warningObj;
    public Collider2D arenaBorder;

    private void Awake()
    {
        arenaBorder = GameObject.Find("ArenaBorder").GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        warningObj = spawnWarning(warningPrefab, arenaBorder.ClosestPoint(gameObject.transform.position), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ColliderContainsPoint(arenaBorder.transform, gameObject.transform.position, true));
        //Debug.Log(arenaBorder.bounds.Contains(gameObject.transform.position));
        
        if (gameObject != null && arenaBorder != null)
        {
            if (arenaBorder.bounds.Contains(gameObject.transform.position))
            {
                Destroy(warningObj);
            }
            else if (!arenaBorder.bounds.Contains(gameObject.transform.position) && arenaBorder.ClosestPoint(gameObject.transform.position) != null && warningObj!=null)
                warningObj.transform.position = arenaBorder.ClosestPoint(gameObject.transform.position);
        }
        else
        {
            Destroy(warningObj);
        }
    }
    GameObject spawnWarning(GameObject warningPrefab, Vector2 warningPos, Quaternion warningRot)
    {
        GameObject warningObj = Instantiate(warningPrefab, warningPos, warningRot);
        return warningObj;
    }
    bool ColliderContainsPoint(Transform ColliderTransform, Vector3 Point, bool Enabled)
    {
        Vector3 localPos = ColliderTransform.InverseTransformPoint(Point);
        if (Enabled && Mathf.Abs(localPos.x) < 0.5f && Mathf.Abs(localPos.y) < 0.5f && Mathf.Abs(localPos.z) < 0.5f)
            return true;
        else
            return false;
    }
    bool checkWarning()
    {
        if (gameObject != null || (!arenaBorder.bounds.Contains(gameObject.transform.position)))
        {
            warningObj.transform.position = arenaBorder.ClosestPoint(gameObject.transform.position);
            return true;
        }
        else
        {
            Destroy(warningObj, 0.5f);
            return false;
        }
    }
    public void destroyWarning()
    {
        if (warningObj != null)
            Destroy(warningObj);

    }

}
