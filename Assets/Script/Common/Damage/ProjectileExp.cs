using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExp : ProjectileDefault
{
    // public float ExpRange;  // 광역 데미지 범위
    // private Vector3 boxSize;
    // BoxCollider box;
    List<GameObject> currentCollisions = new List<GameObject>();
    // Start is called before the first frame update
    protected override void Start()
    {
        // box = transform.GetChild(0).GetComponent<BoxCollider>();
        // boxSize = new Vector3(ExpRange, ExpRange, ExpRange);

    }
    /*
    private void OnDisable()
    {
        box.enabled = true;
    }
    */
    protected override void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == this.gameObject.tag)
        {
            currentCollisions.Add(coll.gameObject);
            // Collider[] explo= Physics.OverlapBox(transform.position, boxSize, transform.rotation);
            
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(true);
            transform.GetChild(transform.childCount - 2).gameObject.SetActive(false);
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            foreach (GameObject gObject in currentCollisions)
            {
                gObject.GetComponent<UnitHp>().Damaged(int.Parse(this.gameObject.name), "Magic");
                currentCollisions.Remove(gObject);
                // coll.SendMessage("Damaged", tempStorage);
                Invoke("False", 1);
            }

            /*
            object[] tempStorage = new object[2];
            tempStorage[0] = int.Parse(this.gameObject.name);
            tempStorage[1] = "Magic";
            */
            /*
            foreach (var col in explo)
            {
                if (col.gameObject.tag == this.gameObject.tag)
                {
                    col.GetComponent<UnitHp>().Damaged(int.Parse(this.gameObject.name), "Magic");
                    // coll.SendMessage("Damaged", tempStorage);
                    Invoke("False", 1);
                }
            }
            */
            // box.enabled = false;
        }
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawSphere(transform.position, ExpRange);
        Gizmos.DrawCube(transform.position, new Vector3(ExpRange, ExpRange, ExpRange));
    }
    */
}
