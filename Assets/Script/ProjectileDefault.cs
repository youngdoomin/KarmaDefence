using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDefault : ProjectileSet
{
    void Start()
    {

        if (isMove == true)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * speed);
            //Destroy(this.gameObject, 10);
        }
        else
        {
            //Destroy(this.gameObject, 1);
        }

        //GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        /*
        if (this.gameObject.tag == "Friendly")
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * speed);

        }
        else
        {
            GetComponent<Rigidbody>().AddForce(-transform.forward * speed);
        }
        */
    }

    // Update is called once per frame


    protected void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == this.gameObject.tag)
        {
            coll.gameObject.SendMessage("Damaged", int.Parse(this.gameObject.name));
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            if (isMove == true)
            {
                transform.parent = coll.transform;
                transform.localPosition = Vector3.zero;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                transform.GetChild(transform.childCount).gameObject.SetActive(true);
                Destroy(this.gameObject, 1);

            }

        }


    }
}
