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
    private void OnEnable()
    {
        waiting = false;
        coll.enabled = true;
        hitCt = 0;
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
            yield return new WaitForSeconds(WaitDestroy);
            gameObject.SetActive(false);
        }
        else { StartCoroutine(AttackDelay()); }
    }

    public void Upgrade(int per)
    {
        DamageAmt += DamageAmt * per / 100;
    }
}
