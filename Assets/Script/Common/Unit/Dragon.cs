using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : UnitHp
{
    public GameObject winObj;

    protected override void Disable()
    {
        winObj.SetActive(true);
        Time.timeScale = 0;
        GameManager.instance.Result();
        gameObject.SetActive(false);
    }
}
