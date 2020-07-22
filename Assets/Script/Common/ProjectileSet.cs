using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSet : MonoBehaviour
{
    public float speed;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
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
    void Update()
    {

    }

    protected void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == this.gameObject.tag)
        {
            coll.gameObject.SendMessage("Damaged", damage);
            Destroy(gameObject);

        }
    
        
    }
}
