using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawExp : MonoBehaviour
{
    public GameObject ExpEf;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explo(float range)
    {
        this.gameObject.transform.parent = null;
        var exp = Instantiate(ExpEf, transform.position, Quaternion.identity);
        Color color = exp.GetComponent<MeshRenderer>().material.color;
        color.a = 0.1f;
        exp.transform.localScale *= range;
    }
}
