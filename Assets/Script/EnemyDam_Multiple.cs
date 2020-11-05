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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
            if(other.gameObject.tag == "Enemy")
            {
                other.gameObject.SendMessage("Damaged", DamageAmt);
                StartCoroutine(AttackDelay());
            }
    }
    IEnumerator AttackDelay()
    {
        if(hitCt < hitMax && !waiting)
        {
            hitCt++;
            yield return new WaitForSeconds(WaitTime);
            waiting = true;
            yield return new WaitForSeconds(WaitTime);
            waiting = false;
        }
        else
        {
            Destroy(gameObject, WaitDestroy);
        }
    }
}
