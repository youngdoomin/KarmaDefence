using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform bossPos;
    public GameObject boss;
    [HideInInspector]
    public bool killed;
    [HideInInspector]
    public int CurrentMoney;
    public GameObject winObj;
    public GameObject loseObj;

    public Image[] starObj;
    public Sprite starSprite;
    public Text[] text_Quest;
    private int starCt;
    //private List<bool> star = new List<bool>{false,false,false };
    private bool cleared;
    [HideInInspector]
    public int spawnCt;
    [HideInInspector]
    public int killCt;
    [HideInInspector]
    public int reinforceCt;
    [HideInInspector]
    public int saveHPCt;
    [HideInInspector]
    public int saveUnitCt;
    [HideInInspector]
    public int thisStage;

    public enum questType
    {
        Clear, // 스테이지 클리어
        Spawn, // 유닛 소환
        Kill, // 적 처치
        Reinforce, // 강화
        SaveHP, // 석상 or 대천사 체력
        SaveUnit // 유닛 살려두기
    }

    public questType[] Quests;

    public List<int> QuestValue;
    void Awake()
    {
        instance = this;

        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        starCt = PlayerPrefs.GetInt("saveStarCt");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBoss()
    {
        Debug.Log("Spawn");
        Instantiate(boss, bossPos);
    }

    public void WinScreen()
    {
        winObj.SetActive(true);
        TimeStop();
        Result();
    }

    public void LoseScreen()
    {
        loseObj.SetActive(true);
        TimeStop();
    }

    void TimeStop()
    {
        Time.timeScale = 0;
    }

    void Result()
    {
        cleared = true;

        for (int i = 0; i < Quests.Length; i++)
        {
            if (Quests[i] == questType.Clear)
            {
                text_Quest[i].text = "스테이지 " + QuestValue[i] + " 클리어 하기";
                if(cleared == true) { CheckStar(i); }
            }
            else if(Quests[i] == questType.Spawn)
            {
                text_Quest[i].text = "천사 유닛 " + QuestValue[i] + " 명 이상 소환하기";
                if (spawnCt > QuestValue[i]) { CheckStar(i); }
            }
            else if (Quests[i] == questType.Kill)
            {
                text_Quest[i].text = "악마 유닛 " + QuestValue[i] + " 마리 처치하기";
                if (killCt > QuestValue[i]) { CheckStar(i); }
            }
            else if (Quests[i] == questType.Reinforce)
            {
                if (reinforceCt > QuestValue[i]) { CheckStar(i); }
            }
            else if (Quests[i] == questType.SaveHP)
            {
                if (saveHPCt > QuestValue[i]) { CheckStar(i); }
            }
            else if (Quests[i] == questType.SaveUnit)
            {
                if (saveUnitCt > QuestValue[i]) { CheckStar(i); }
            }
        }
    }

    void CheckStar(int i)
    {
        starCt++;
        PlayerPrefs.SetInt("saveStarCt", starCt);
        //star[i] = true;
        starObj[i].sprite = starSprite;
    }
}
