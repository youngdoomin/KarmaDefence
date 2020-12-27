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
            else if(type == Type.Skill1)
               amt = upObj.GetComponent<SkillMgr>().Time_shield;
            else if (type == Type.Skill2)
                amt = upObj.GetComponent<EnemyDam>().DamageAmt;
            else
                amt = upObj.GetComponent<EnemyDam_Multiple>().DamageAmt;
            currTier++;

            amt += amt * upPer / 100;

            if (price.Length - 1 < currTier)
            {
                txt.text = "Max";
                b.onClick.RemoveAllListeners();
                return;
            }
            Refresh();
        }

    }

    void IncPercent()
    {

    }

    void Refresh()
    {
        txt.text = price[currTier].ToString();
        
    }
}
