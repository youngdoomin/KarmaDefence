using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : UnitHp
{
    protected override void Disable()
    {
        Time.timeScale = 0;
        GameManager.instance.Result();
        gameObject.SetActive(false);
    }
}
