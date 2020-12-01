using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGold : UnitHp
{
    public int incGold;
    // Start is called before the first frame update

    protected override void UnitDeath()
    {
        GameManager.instance.killCt++;
        GameManager.instance.Gold += incGold;
    }
}
