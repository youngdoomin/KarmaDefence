using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQHP : UnitHp
{
    public GameObject loseObj;
    protected override void Disable()
    {
        loseObj.SetActive(true);
        Time.timeScale = 0;
        Destroy(gameObject);
    }
}
