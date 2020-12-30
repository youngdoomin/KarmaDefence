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
                GameManager.instance.Invincible = true;
                StartCoroutine(shieldCt(spawn));
                
            }
        }
    }

    IEnumerator shieldCt(GameObject obj)
    {
        yield return new WaitForSeconds(Time_shield);
        Destroy(obj);
        GameManager.instance.Invincible = false;
    }

    public void LightRain(GameObject Rain)
    {
        var player = new Vector3(this.gameObject.transform.position.x, 0, 0);
        Instantiate(Rain, player + RainPos.localPosition, Quaternion.identity);
    }
    public void LightExp(GameObject Explosion)
    {
        StartCoroutine(xScaleInc(Explosion));
    }

    IEnumerator xScaleInc(GameObject Explosion)
    {
        var xScale = 0;
        var ExpSkill = Instantiate(Explosion, ExpPos.position, Quaternion.identity);
        int i;
        if(RainPos.localPosition.x > 0) { i = 1; }
        else 
        { 
            i = -1;
            ExpSkill.transform.GetChild(1).rotation = Quaternion.Euler(0, 180, 0);
            //Quaternion.Inverse(ExpSkill.transform.GetChild(1).localRotation);

        }

        ExpSkill.transform.GetChild(0).localPosition = new Vector3(0.5f * i, 0, 0);
        //var particle = ExpSkill.transform.GetChild(1);
        //particle.rotation = Quaternion.Euler(0, particle.localRotation.y * i, 0);
        //Debug.Log(particle.localRotation);
        //ExpSkill.transform.SetParent(ExpPos);
        //ExpSkill.transform.localPosition = new Vector3(Mathf.Abs(0.5f) * i, 0, 0);


        while (ExpSkill.transform.localScale.x < ExpAxisX)
         {
             ExpSkill.transform.localScale += new Vector3(xScale, 0, 0);
             xScale++;
             yield return new WaitForSeconds(SecAxisX);
         }
         
        yield return new WaitForSeconds(0.1f);
        Destroy(ExpSkill);
    }
}
