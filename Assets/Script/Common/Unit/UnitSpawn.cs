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
        var random = Random.Range(0, Units.Length);
        var Spawn = Instantiate(Units[random], SpawnPos);
        yield return new WaitForSeconds(SpawnDelay[random]);
        Debug.Log(SpawnDelay[random]);
        StartCoroutine(AutoSpawner());
    }

    public void Spawner(GameObject i)
    {
        Vector3 Pos = new Vector3(SpawnPos.position.x, SpawnPos.position.y, Random.Range(-zAxis, zAxis));
        var Spawn = Instantiate(i, Pos, Quaternion.identity);
        GameManager.instance.spawnCt++;
    }

}
