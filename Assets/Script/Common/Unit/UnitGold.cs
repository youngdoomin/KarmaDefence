using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGold : UnitHp
{
    public int[] incGold;
    // Start is called before the first frame update

    protected override void Disable()
    {
        Hp = (int)initHp;
        GameManager.instance.killCt++;
        GameManager.instance.Gold += incGold[(int)PlayerPrefs.GetFloat("level") - 1];
        gameObject.SetActive(false);
    }
}
