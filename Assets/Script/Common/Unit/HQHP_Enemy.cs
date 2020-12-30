using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQHP_Enemy : UnitHp
{
    public Transform bossPos;
    // public GameObject boss;

    protected override void Disable()
    {
        //Instantiate(boss, bossPos);
        bossPos.GetChild(0).gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
