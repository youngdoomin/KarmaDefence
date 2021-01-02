using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMgr : MonoBehaviour
{
    public GameObject[] SkillObj;

    public Transform ShieldPos;
    public Transform RainPos;
    public Transform ExpPos;
    public Transform RainTargetPos;

    public float Time_shield;
    public float ExpAxisX;

    public float SecAxisX;
    private IEnumerator coroutine;

    int unitCt;

    private void Start()
    {
        unitCt = GameObject.Find("MyHq").GetComponent<UnitSpawn>().spawnCt * GameObject.Find("MyHq").GetComponent<UnitSpawn>().Units.Length;
        shieldPool();
        skillPool(1, RainPos);
        skillPool(2, ExpPos);

    }

    void shieldPool()
    {
        for (int j = 0; j < unitCt; j++)
        {
            var spawn = Instantiate(SkillObj[0], transform.position, Quaternion.identity);
            spawn.transform.parent = ShieldPos;
            spawn.SetActive(false);
        }
    }

    void skillPool(int i, Transform tr)
    {
        var spawn = Instantiate(SkillObj[i], transform.position, Quaternion.identity);
        spawn.transform.parent = tr;
        spawn.SetActive(false);
    }

    public void Mercy()
    {
        Vector3 reset = new Vector3(0, 0, 0);
        GameObject[] friendly = GameObject.FindGameObjectsWithTag("Friendly");
        for (int i = 0; i < friendly.Length; i++)
        {
            if (friendly[i].layer == 13)
            {
                if (friendly[i].transform.childCount > 1)
                {
                    friendly[i].transform.GetChild(1).gameObject.SetActive(false);
                    friendly[i].transform.GetChild(1).gameObject.transform.parent = ShieldPos;
                }

                var spawn = ShieldPos.transform.GetChild(i);
                spawn.transform.parent = friendly[i].transform;
                spawn.transform.localPosition = reset;
                spawn.gameObject.SetActive(true);
                coroutine = shieldCt(spawn.gameObject);
                if (GameManager.instance.Invincible)
                {
                    StopCoroutine(coroutine);

                }

                //if(coroutine != null)
                //StopCoroutine(coroutine);
                StartCoroutine(coroutine);
            }
        }

        GameManager.instance.Invincible = true;
    }

    IEnumerator shieldCt(GameObject obj)
    {
        yield return new WaitForSeconds(Time_shield);
        try
        {
            if (obj.transform.parent.transform.childCount <= 2)
                GameManager.instance.Invincible = false;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        obj.SetActive(false);
    }


    public void LightRain()
    {
        var player = new Vector3(this.gameObject.transform.position.x, 0, 0);
        for (int i = 0; i < RainPos.transform.childCount; i++)
        {
            var spawn = RainPos.transform.GetChild(i);
            if (!spawn.gameObject.activeSelf)
            {
                spawn.gameObject.SetActive(true);
                spawn.transform.position = player + RainTargetPos.localPosition;
                break;
            }
            else if(i + 1 == RainPos.transform.childCount)
            { 
                skillPool(1, RainPos);
                //LightRain();
            }
            
        }
        // Instantiate(SkillObj[1], player + RainTargetPos.localPosition, Quaternion.identity);
    }
    public void LightExp()
    {
        StartCoroutine(xScaleInc());
    }

    IEnumerator xScaleInc()
    {
        /*
        var xScale = 0;
        var ExpSkill = Instantiate(Explosion, ExpPos.position, Quaternion.identity);
        int i;
        if(RainPos.localPosition.x > 0) { i = 1; }
        else 
        { 
            i = -1;
            ExpSkill.transform.GetChild(1).rotation = Quaternion.Euler(0, 180, 0);

        }

        ExpSkill.transform.GetChild(0).localPosition = new Vector3(0.5f * i, 0, 0);

        while (ExpSkill.transform.localScale.x < ExpAxisX)
         {
             ExpSkill.transform.localScale += new Vector3(xScale, 0, 0);
             xScale++;
             yield return new WaitForSeconds(SecAxisX);
         }
         */
        yield return new WaitForSeconds(0.1f);
        // Destroy(ExpSkill);
    }
}
