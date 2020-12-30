using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reinforce_Grid : MonoBehaviour
{
    float amt;
    public int upPer;
    public enum Type
    {
        Unit,
        Skill1,
        Skill2,
        Skill3
    }

    public Type type;
    public GameObject upObj;
    public int[] price;
    Text txt;
    Button b;
    int currTier;

    void Start()
    {
        b = GetComponent<Button>();
        b.onClick.AddListener(() => Upgrade());
        txt = transform.GetChild(0).GetComponent<Text>();
        Refresh();

        if (type == Type.Skill1)
            amt = upObj.GetComponent<SkillMgr>().Time_shield;
        else if (type == Type.Skill2)
        {
            amt = upObj.GetComponent<EnemyDam_Multiple>().DamageAmt;

        }
        else if (type == Type.Skill3)
        {
            amt = upObj.transform.GetChild(0).GetComponent<EnemyDam>().DamageAmt;
        }
    }

    void Update()
    {

    }

    public void Upgrade()
    {
        if (price[currTier] <= GameManager.instance.Gold)
        {
            GameManager.instance.Gold -= price[currTier];
            if (type == Type.Unit)
                upObj.GetComponent<Unit>().Upgrade(upPer);
            
            currTier++;

            amt += amt * upPer / 100;
            if (type == Type.Skill1)
                upObj.GetComponent<SkillMgr>().Time_shield = amt;
            else if (type == Type.Skill2)
            {
                upObj.GetComponent<EnemyDam_Multiple>().DamageAmt = (int)amt;

            }
            else if (type == Type.Skill3)
            {
                upObj.transform.GetChild(0).GetComponent<EnemyDam>().DamageAmt = (int)amt;
            }


            if (price.Length - 1 < currTier)
            {
                txt.text = "Max";
                b.onClick.RemoveAllListeners();
                return;
            }
            Refresh();
        }

    }


    void Refresh()
    {
        txt.text = price[currTier].ToString();
        
    }
}
