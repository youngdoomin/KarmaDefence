using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExp : ProjectileSet
{
    public float ExpRange;  // 광역 데미지 범위
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    new protected void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == this.gameObject.tag)
        {
            //this.gameObject.BroadcastMessage("SizeCon");
            Collider[] explo = Physics.OverlapSphere(gameObject.transform.position, ExpRange);
            foreach (var col in explo)
            {
                if (col.gameObject.tag == this.gameObject.tag)
                {
                    col.gameObject.SendMessage("Damaged", damage);
                    this.gameObject.BroadcastMessage("Explo", ExpRange);
                    //Gizmos.DrawSphere(col.gameObject.transform.position, ExpRange);
                    Destroy(gameObject);
                }
            }
        }
    }
}
