using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitSpawn : MonoBehaviour
{
    /*
    GameObject PlayerFinder;

    public GameObject[] tilePooledObjects;
    private GameObject spawnedTile;
    public GameObject[] pooledObjects;
    
    public GameObject enemy;

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
    */
    private GameObject unitObj;
    public int spawnCt;


    public bool IsSpawn;
    public float[] SpawnDelay;
    public Transform SpawnPos;
    public GameObject[] Units;
    public GameObject Hp_bar;
    public float zAxis;
    public int chooseUnit;
    // Start is called before the first frame update

    void Start()
    {
        PoolSpawn();
        // GetPooledObject(false, enemySpawnPos.transform);

        /*
        for (int i = 0; i < tilePooledObjects.Length; i++)
        {
            spawnedTile = Instantiate(tilePooledObjects[i], transform.position, Quaternion.identity);
            spawnedTile.transform.parent = tileSpawnPos.transform;
            spawnedTile.SetActive(false);
        } // 인스펙터에 있는 타일 생성
        */
        // GetRandomPooledObject();

        if (IsSpawn == true)
        StartCoroutine(AutoSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PoolSpawn()
    {
        for (int i = 0; i < spawnCt; i++) // 인스펙터에 있는 적 생성
        {
            for (int j = 0; j < Units.Length; j++)
            {
                unitObj = Instantiate(Units[j], transform.position, Quaternion.identity);
                unitObj.transform.parent = SpawnPos.transform;
                unitObj.SetActive(false);

            }
        }
    }
    IEnumerator AutoSpawner()
    {
        Vector3 Pos = new Vector3(SpawnPos.position.x, SpawnPos.position.y, Random.Range(-zAxis, zAxis));
        int random;
        if(chooseUnit != 0)
        {
            random = chooseUnit - 1;
        }
        else { random = Random.Range(0, Units.Length); }
        Debug.Log(random);
        // Instantiate(Units[random], Pos, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        
        for (int i = 0; i < Units.Length * spawnCt; i += Units.Length)
        {
            if (i < random) { i = random; }
            var spawnObj = SpawnPos.transform.GetChild(i).gameObject;
            if (!spawnObj.activeSelf)
            {
                spawnObj.SetActive(true);
                spawnObj.transform.position = Pos;
                //spawnObj.transform.rotation = transform.rotation * Quaternion.Euler(0f, 180f, 0f);
                yield return new WaitForSeconds(SpawnDelay[random]);
                break;
            }
            
            else if (i + Units.Length >= Units.Length * spawnCt)
            {
                spawnCt += spawnCt;
                PoolSpawn();
            }
            

        }
        StartCoroutine(AutoSpawner());
        //Debug.Log(SpawnDelay[random]);
    }

    public void Spawner(int idx)
    {
        Vector3 Pos = new Vector3(SpawnPos.position.x, SpawnPos.position.y, Random.Range(-zAxis, zAxis));
        Debug.Log(idx);
        for (int i = 0; i < Units.Length * spawnCt; i += Units.Length)
        {
            if(i < idx) { i = idx; }
            var spawnObj = SpawnPos.transform.GetChild(i).gameObject;
            if (!spawnObj.activeSelf)
            {
                spawnObj.SetActive(true);
                spawnObj.transform.position = Pos;
                break;
            }

            else if (i + Units.Length >= Units.Length * spawnCt)
            {
                spawnCt += spawnCt;
                PoolSpawn();
            }
        }
        //Instantiate(obj, Pos, Quaternion.identity);
        GameManager.instance.spawnCt++;
    }

}
