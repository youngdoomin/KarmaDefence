using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDefault : ProjectileSet
{
    protected virtual void Start()
    {

        if (isMove == true)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        }
        Invoke("False", 10);

       
    }


    protected virtual void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == this.gameObject.tag)
        {
            coll.GetComponent<UnitHp>().Damaged(int.Parse(this.gameObject.name));
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            if (isMove == true)
            {
                //transform.parent = coll.transform;
                //transform.localPosition = Vector3.zero;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
                Invoke("False", 1);
            }

        }


    }

    protected void False()
    {
        this.gameObject.SetActive(false);
    }
}
