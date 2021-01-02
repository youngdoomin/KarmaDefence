using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitSpawn : MonoBehaviour
{
    public bool IsSpawn;
    public float[] SpawnDelay;
    public Transform SpawnPos;
    public Transform p_SpawnPos;

    public GameObject[] projectileObj;
    public GameObject[] Units;
    public GameObject Hp_bar;
    public int spawnCt;
    public float zAxis;
    public int chooseUnit;

    private GameObject unitObj;

    void Start()
    {
        PoolSpawn(Units, SpawnPos);
        PoolSpawn(projectileObj, p_SpawnPos);
        

        if (IsSpawn == true)
        StartCoroutine(AutoSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PoolSpawn(GameObject[] obj, Transform tr)
    {
        for (int i = 0; i < spawnCt; i++) // 인스펙터에 있는 적 생성
        {
            for (int j = 0; j < obj.Length; j++)
            {
                unitObj = Instantiate(obj[j], transform.position, Quaternion.identity);
                unitObj.transform.parent = tr.transform;
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
                PoolSpawn(Units, SpawnPos);
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
                PoolSpawn(Units, SpawnPos);
            }
        }
        //Instantiate(obj, Pos, Quaternion.identity);
        GameManager.instance.spawnCt++;
    }

}
