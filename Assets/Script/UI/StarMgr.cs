using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarMgr : MonoBehaviour
{
    public Text starText;

    //스텟
    public int Up_Hp;
    public int UP_WalkSpeed;
    public int Up_Damage;
    public int Up_AttackSpeed;
    public int Up_Unit_CDT;
    public int Up_Skill1_Duration;
    public int Up_Skill1_Shield;
    public int Up_Skill2_Damage;
    public int Up_Skill3_Damage;
    public int Up_Skill123_CDT;
    public int Up_Angel_Statue_Hp;

    public struct UpgradeInfo
    {

        //public questType Type;
        //[System.Serializable]
        public enum UpgradeType
        {
            Stat,       // 스텟 
            CoolTime,   // 쿨타임
            Duration,   // 지속 시간
            Protect,    // 쉴드 량
            Damage,     // 데미지
        }

        public UpgradeType Type;
        public string Stat;
        public int UpgradeValue;
        public GameObject group;
        public Text description;

    }

    UpgradeInfo[] upgradeInfo;
    // Start is called before the first frame update
    void Start()
    {
        starText.text = GameManager.instance.starCt.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade(int star)
    {
        if(GameManager.instance.starCt >= star)
        {
            GameManager.instance.starCt -= star;

        }

    }

    public void ShowDescription()
    {
        for (int i = 0; i < upgradeInfo.Length; i++)
        {
            if (upgradeInfo[i].Type == UpgradeInfo.UpgradeType.Stat)
            {
                upgradeInfo[i].description.text = upgradeInfo[i].Stat + " 증가 : 모든 유닛의 " + upgradeInfo[i].Stat + "이/가 " + upgradeInfo[i].UpgradeValue + " 만큼 증가합니다.";
               
            }
            else if (upgradeInfo[i].Type == UpgradeInfo.UpgradeType.CoolTime)
            {
                upgradeInfo[i].description.text = upgradeInfo[i].Stat + " 감소 : 모든 유닛의 " + upgradeInfo[i].Stat + "이/가 " + upgradeInfo[i].UpgradeValue + " 만큼 감소합니다.";

            }
            else if (upgradeInfo[i].Type == UpgradeInfo.UpgradeType.Duration)
            {
                upgradeInfo[i].description.text = upgradeInfo[i].Stat + " 증가 : 구원 스킬의 " + upgradeInfo[i].Stat + "이/가 " + upgradeInfo[i].UpgradeValue + " 만큼 증가합니다.";
            }
            else if (upgradeInfo[i].Type == UpgradeInfo.UpgradeType.Protect)
            {
                upgradeInfo[i].description.text = upgradeInfo[i].Stat + " 증가 : 구원 스킬의 " + upgradeInfo[i].Stat + "이/가 " + upgradeInfo[i].UpgradeValue + " 만큼 증가합니다.";
            }
            else if (upgradeInfo[i].Type == UpgradeInfo.UpgradeType.Damage)
            {
                upgradeInfo[i].description.text = upgradeInfo[i].Stat + " 데미지 증가 : 해당 스킬의 " + upgradeInfo[i].Stat + "이/가 " + upgradeInfo[i].UpgradeValue + " 만큼 증가합니다.";
            }
        }

    }
}
