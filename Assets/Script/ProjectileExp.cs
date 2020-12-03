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
        Destroy(this.gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == this.gameObject.tag)
        {
            //this.gameObject.BroadcastMessage("SizeCon");
            Collider[] explo = Physics.OverlapSphere(gameObject.transform.position, ExpRange);
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
            transform.GetChild(transform.childCount - 2).gameObject.SetActive(false);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            foreach (var col in explo)
            {
                if (col.gameObject.tag == this.gameObject.tag)
                {
                    col.gameObject.SendMessage("Damaged", int.Parse(this.gameObject.name));
                    //this.gameObject.BroadcastMessage("Explo", ExpRange);
                    //Gizmos.DrawSphere(col.gameObject.transform.position, ExpRange);
                    
                    Destroy(gameObject, 1f);
                }
            }
        }
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, ExpRange);
    }
    */
}
