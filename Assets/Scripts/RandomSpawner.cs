using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [Header("- - - References - - -")]

    [SerializeField] gameState gameState;
    [SerializeField] GameObject Battery;
    [SerializeField] Collider2D[] BatterySpawnArea;
    [SerializeField] float x1, x2, y1, y2;


    [Header("- - - Variables - - -")]
    [SerializeField] float BatterySpawnRate=5f;

    void Start()
    {
        StartCoroutine(BatterySpawn());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator BatterySpawn()
    {
        //Debug.Log(gameState.get_isPlayerAlive());
        while (gameState.get_isPlayerAlive())
        {
            //Debug.Log("anskm");
            spawnInArea(Battery, BatterySpawnArea);
            yield return new WaitForSeconds(BatterySpawnRate);
        }
    }
    void spawnInArea(GameObject spawnPrefab, Collider2D[] area)
    {
        Vector2 spawnPos = findSpawnArea(x1,x2, y1, y2);
        spawnRocket1(Battery, spawnPos);
    }

    Vector2 findSpawnArea(float x1, float x2,float y1,float y2)
    {
        var px = Random.Range(-7f, 7);
        var py = Random.Range(-3.5f, 3.5f);
        Vector2 pos = new Vector3(px, py);
        //Debug.Log(pos);
        return pos;
    }

    void spawnRocket1(GameObject spawnPrefab, Vector2 spawnPos)
    {
        GameObject rocket = Instantiate(spawnPrefab, spawnPos, Quaternion.identity);
    }
}
