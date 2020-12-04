using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMgr : MonoBehaviour
{
    public Transform RainPos;
    public Transform ExpPos;

    public float Time_shield;
    public float ExpAxisX;

    public float SecAxisX;
    /*
    [System.Serializable]
    public enum Skills
    {
        Mercy,
        LightRain,
        LightExp
    }
    */

    //public Skills Skill;

    public void Mercy(GameObject Shield)
    {
        Vector3 reset = new Vector3(0, 0, 0);
        GameObject[] friendly = GameObject.FindGameObjectsWithTag("Friendly");
        foreach (GameObject fUnit in friendly)
        {
            if (fUnit.layer == 13 && fUnit.transform.childCount <= 1)
            {
                var spawn = Instantiate(Shield, transform.position, Quaternion.identity);
                spawn.transform.parent = fUnit.transform;
                spawn.transform.localPosition = reset;
                fUnit.GetComponent<UnitHp>().Protect(Time_shield); // SendMessage("Protect", Time_shield);
            }
        }
    }

    public void LightRain(GameObject Rain)
    {
        Instantiate(Rain, RainPos.localPosition, Quaternion.identity);
    }
    public void LightExp(GameObject Explosion)
    {
        StartCoroutine(xScaleInc(Explosion));
    }

    IEnumerator xScaleInc(GameObject Explosion)
    {
        var xScale = 0;
        var ExpSkill = Instantiate(Explosion, ExpPos.position, Quaternion.identity);
        while(xScale < ExpAxisX)
        {
            ExpSkill.transform.localScale = new Vector3(xScale, 1, 1);
            xScale++;
            yield return new WaitForSeconds(SecAxisX);
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(ExpSkill);

    }
}
