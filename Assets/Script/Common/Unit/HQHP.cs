using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQHP : UnitHp
{
    public int incHp;
    public GameObject loseObj;

    protected override void SetHp()
    {
        for (int i = 1; i < PlayerPrefs.GetInt("level"); i++)
        {
            Hp += incHp;
        }
        initHp = Hp;
    }
    protected override void Disable()
    {
        loseObj.SetActive(true);
        Time.timeScale = 0;
        gameObject.SetActive(false);
    }
}
