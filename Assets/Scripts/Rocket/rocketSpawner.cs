using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketSpawner : MonoBehaviour
{

    [Header("- - - References - - -")]
    [SerializeField] GameObject rocketPrefab;
    [SerializeField] GameObject bigRocketPrefab;
    [SerializeField] Transform[] bigRocketSpawnPos;
    [SerializeField] GameObject[] Targets;
    [SerializeField] GameObject forceTarget;
    [SerializeField] Collider2D[] SpawnAreas;
    [SerializeField] AudioManager audioManager;

    [Header("- - - Variables - - -")]
    [SerializeField] float nonRandomDelay;
    [SerializeField] bool isDelayRandom;
    [SerializeField] float randomDelayRange1, randomDelayRange2;
    [SerializeField] float bigRandomDelayRange1, bigRandomDelayRange2;
    [SerializeField] bool rocketsActive = true;
    [SerializeField] float waitBigRocketTime = 20f;

    //private int spawnNum;
    //[SerializeField] Camera cam;
    private void Awake()
    {
    }

    // Update is called once per frame
    private void Start()
    {
        StartCoroutine(spawnManager(rocketPrefab, randomDelayRange1, randomDelayRange2));
        StartCoroutine(bigSpawnManager(bigRocketPrefab, bigRandomDelayRange1, bigRandomDelayRange2));
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            spawnInArea(rocketPrefab, SpawnAreas);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Transform bigRocketSpawnT = setBigRocketSpawn(bigRocketSpawnPos);
            spawnBigRocket(bigRocketPrefab, bigRocketSpawnT.position, bigRocketSpawnT.rotation);
        }
    }

    void spawnRocket()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1f;
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        GameObject rocket = Instantiate(rocketPrefab, objectPos, Quaternion.identity);
        rocket.GetComponent<chooseTarget>().setAllTargets(Targets[0], Targets[1]);
    }

    void spawnInArea(GameObject spawnPrefab, Collider2D[] spawnColliders)
    {
        Collider2D myColl;
        Vector2 spawnPos = findSpawnArea(spawnColliders, out myColl);
        Quaternion myRot = findRot(myColl);
        spawnRocket1(spawnPrefab, spawnPos, myRot);
        //audioManager.PlayRocketStart();
    }

    Quaternion findRot(Collider2D myColl)
    {
        if (myColl == SpawnAreas[0])
        {
            return Quaternion.Euler(0, 0, 0);
        }
        else if (myColl == SpawnAreas[1])
        {
            return Quaternion.Euler(0, 0, -180);
        }
        else if (myColl == SpawnAreas[2])
        {
            return Quaternion.Euler(0, 0, -90);
        }
        else if(myColl == SpawnAreas[3])
        {
            return Quaternion.Euler(0, 0, 90);
        }
        else//problem
        {
            return Quaternion.identity;
        }
    }

    public Vector2 findSpawnArea(Collider2D[] colliders, out Collider2D myColl)
    {
        int spawnNum = Random.Range(0, colliders.Length);
        Collider2D activeCollider = colliders[spawnNum];
        myColl = activeCollider;
        var bounds = activeCollider.bounds;
        var px = Random.Range(bounds.min.x, bounds.max.x);
        var py = Random.Range(bounds.min.y, bounds.max.y);
        Vector2 pos = new Vector3(px, py);
        return pos;
    }

    IEnumerator spawnManager(GameObject rocketPrefab, float randomDelayRange1, float randomDelayRange2)
    {
        while(rocketsActive)
        {
            if (isDelayRandom == true)
            {
                float delayTime = Random.Range(randomDelayRange1, randomDelayRange2);
                yield return new WaitForSeconds(delayTime);
                spawnInArea(rocketPrefab, SpawnAreas);
            }
            else if(isDelayRandom == false)
            {
                yield return new WaitForSeconds(nonRandomDelay);
                if (nonRandomDelay > 0.2)
                {
                    nonRandomDelay = nonRandomDelay - (nonRandomDelay/100);
                }
                spawnInArea(rocketPrefab, SpawnAreas);
            }
        }
    }

    IEnumerator bigSpawnManager(GameObject rocketPrefab, float randomDelayRange1, float randomDelayRange2)
    {
        yield return new WaitForSeconds(waitBigRocketTime);
        while (rocketsActive)
        {
            /*if (isDelayRandom == true)
            {*/
                float delayTime = Random.Range(randomDelayRange1, randomDelayRange2);
                yield return new WaitForSeconds(delayTime);
                Transform bigRocketSpawnT = setBigRocketSpawn(bigRocketSpawnPos);
                spawnBigRocket(bigRocketPrefab, bigRocketSpawnT.position, bigRocketSpawnT.rotation);
            /*}
            else if (isDelayRandom == false)
            {
            yield return new WaitForSeconds(nonRandomDelay);
            if (nonRandomDelay > 0.2)
            {
                nonRandomDelay = nonRandomDelay - (nonRandomDelay / 300);
            }
            Transform bigRocketSpawnT = setBigRocketSpawn(bigRocketSpawnPos);
            spawnBigRocket(bigRocketPrefab, bigRocketSpawnT.position, bigRocketSpawnT.rotation);
            }*/
        }
    }

    void spawnRocket1(GameObject spawnPrefab, Vector2 spawnPos, Quaternion rot)
    {
        GameObject rocket = Instantiate(spawnPrefab, spawnPos, rot);
        //warningObj = spawnWarning(warningPrefab, arenaBorder.ClosestPoint(rocket.transform.position), Quaternion.identity);
        rocket.GetComponent<chooseTarget>().setAllTargets(Targets[0], Targets[1]);
        rocket.GetComponent<chooseTarget>().set_forceTarget(forceTarget);
    }

    void spawnBigRocket(GameObject spawnPrefab, Vector2 spawnPos, Quaternion bigRot)
    {
        GameObject rocket = Instantiate(spawnPrefab, spawnPos, bigRot);
        rocket.GetComponent<bigRocket>().smallRocketTarget = forceTarget;
        //warningObj = spawnWarning(warningPrefab, arenaBorder.ClosestPoint(rocket.transform.position), Quaternion.identity);
        //rocket.GetComponent<chooseTarget>().setAllTargets(Targets[0], Targets[1]);
        //rocket.GetComponent<chooseTarget>().set_forceTarget(forceTarget);
    }
    Transform setBigRocketSpawn(Transform[] posArray)
    {
        int randNum = Random.Range(0, posArray.Length);
        return posArray[randNum];
    }
}
