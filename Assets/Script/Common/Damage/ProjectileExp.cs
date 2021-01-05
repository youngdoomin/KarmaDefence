using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExp : ProjectileDefault
{
    public float ExpRange;  // 광역 데미지 범위
    private Vector3 boxSize;
    // Start is called before the first frame update
    protected override void Start()
    {
        boxSize = new Vector3(ExpRange, ExpRange, 1000);
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        Invoke("False", 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == this.gameObject.tag)
        {
            Collider[] explo = Physics.OverlapBox(transform.position, boxSize, transform.rotation);
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
            transform.GetChild(transform.childCount - 2).gameObject.SetActive(false);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            foreach (var col in explo)
            {
                if (col.gameObject.tag == this.gameObject.tag)
                {
                    coll.GetComponent<UnitHp>().Damaged(int.Parse(this.gameObject.name));
                    Invoke("False", 1);
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
