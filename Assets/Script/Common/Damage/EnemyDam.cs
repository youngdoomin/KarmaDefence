using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDam : MonoBehaviour
{
    public bool IsDestroy;
    public int DamageAmt;
    public float WaitDestroy;
    // Start is called before the first frame update
    void Start()
    {
        DamageAmt += (int)PlayerPrefs.GetFloat("skill3");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<UnitHp>().Damaged(DamageAmt);

        }
        if (IsDestroy == true)
            gameObject.SetActive(false);
    }

    public void Upgrade(int per)
    {
        DamageAmt += DamageAmt * per / 100;
    }
}
