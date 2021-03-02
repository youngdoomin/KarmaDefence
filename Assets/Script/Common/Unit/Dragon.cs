using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : UnitHp
{
    protected override void Disable()
    {
        SoundManager.instance.PlaySE(SoundManager.instance.bossDie);
        Time.timeScale = 0;
        GameManager.instance.Win();
        GameManager.instance.Result();
        gameObject.SetActive(false);
    }
}
