using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    GameObject PlayerFinder;

    public GameObject[] tilePooledObjects;
    private GameObject spawnedTile;
    public GameObject[] pooledObjects;
    private GameObject onlyObj;

    public GameObject enemy;
    public int spawnCt;

    public Transform tileSpawnPos;
    public Transform enemySpawnPos;
    public Transform itemSpawnPos;
    public float xAxisRandom;
    public float tileDelay;
    public float enemyDelay;
    public float itemDelay;
    public float jamDelay;

    float xAxis;
    int poolCt;


    void Start()
    {
        tileSpawnPos = GameObject.Find("Block").transform;
        enemySpawnPos = GameObject.Find("Enemy").transform;
        PlayerFinder = GameObject.Find("Player");

        Debug.LogFormat("타일 딜레이 : {0}, 딜레이 {1}", tileDelay, enemyDelay);

        for (int i = 0; i < spawnCt; i++) // 인스펙터에 있는 적 생성
        {
            onlyObj = Instantiate(enemy, transform.position, Quaternion.identity);
            onlyObj.transform.parent = enemySpawnPos.transform;
            onlyObj.SetActive(false);
        }
        GetPooledObject(false, enemySpawnPos.transform);

        for (int i = 0; i < tilePooledObjects.Length; i++)
        {
            spawnedTile = Instantiate(tilePooledObjects[i], transform.position, Quaternion.identity);
            spawnedTile.transform.parent = tileSpawnPos.transform;
            spawnedTile.SetActive(false);
        } // 인스펙터에 있는 타일 생성
        GetRandomPooledObject();
    }

    public GameObject GetRandomPooledObject()
    {
        int randomIndex = 0;


        while (true)
        {

            Debug.Log("Index" + randomIndex);

            if (!tileSpawnPos.transform.GetChild(randomIndex).gameObject.activeInHierarchy) { break; }
        }

        Transform go = tileSpawnPos.transform.GetChild(randomIndex);

        go.gameObject.transform.position = new Vector3(transform.position.x, PlayerFinder.transform.position.y - 30, transform.position.z);
        go.gameObject.SetActive(true);

        return null;
    }


    public GameObject GetPooledObject(bool isRandom, Transform tr)
    {
        Debug.Log(tr);
        xAxis = Random.Range(-xAxisRandom, xAxisRandom);

        if (isRandom)
        {
            int randomIndex = Random.Range(0, tr.transform.childCount - 1);
            int tryCt = 0;
            while (tr.GetChild(randomIndex).gameObject.activeInHierarchy == true)
            {
                randomIndex = Random.Range(0, tr.transform.childCount);

                tryCt++;
                if (tryCt > 50)
                {
                    Debug.Log("too many attempts");
                    for (int i = 0; i < tr.childCount; i++)
                    { tr.GetChild(i).gameObject.SetActive(false); }
                }
            }
            poolCt = randomIndex;
        }
        else if (poolCt >= tr.transform.childCount)
        { poolCt = 0; }

        Transform go = tr.GetChild(poolCt);
        go.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        go.GetComponent<BoxCollider2D>().enabled = true;
        go.gameObject.transform.position = new Vector3(xAxis, PlayerFinder.transform.position.y - 40 - xAxis, transform.position.z);
        go.gameObject.SetActive(true);
        Debug.Log(go.gameObject);
        if (tr.gameObject.tag == "Enemy")
        { poolCt++; }

        return null;
    }

    public void ObjDestroy(string str)
    {
        if (str == "enemy")
        { GetPooledObject(false, enemySpawnPos.transform); }
    }
    public void TileDestroy()
    { Invoke("GetRandomPooledObject", tileDelay); }
    
}
