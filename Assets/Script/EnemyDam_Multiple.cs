using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDam_Multiple : MonoBehaviour
{
    public int DamageAmt;
    public float WaitTime;
    public float WaitDestroy;
    public int hitMax;
    private int hitCt;
    private bool waiting;

    BoxCollider coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider>();
        StartCoroutine(AttackDelay());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<UnitHp>().Damaged(DamageAmt);
        }
    }

    IEnumerator AttackDelay()
    {

        Debug.Log("coll");
        waiting = !waiting;
        coll.enabled = waiting;
        if (waiting)
        {
            hitCt++;
            yield return new WaitForSeconds(WaitTime);
        }

        if (hitCt >= hitMax)
        {
            Destroy(gameObject, WaitDestroy);
        }
        else { StartCoroutine(AttackDelay()); }
    }
}
