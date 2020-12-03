using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitSpawn : MonoBehaviour
{
    public bool IsSpawn;
    public float[] SpawnDelay;
    public Transform SpawnPos;
    public GameObject[] Units;
    public float zAxis;
    public int chooseUnit;
    // Start is called before the first frame update

    void Start()
    {
        if(IsSpawn == true)
        StartCoroutine(AutoSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Instantiate(Units[random], Pos, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        yield return new WaitForSeconds(SpawnDelay[random]);
        Debug.Log(SpawnDelay[random]);
        StartCoroutine(AutoSpawner());
    }

    public void Spawner(GameObject i)
    {
        Vector3 Pos = new Vector3(SpawnPos.position.x, SpawnPos.position.y, Random.Range(-zAxis, zAxis));
        Instantiate(i, Pos, Quaternion.identity);
        GameManager.instance.spawnCt++;
    }

}
