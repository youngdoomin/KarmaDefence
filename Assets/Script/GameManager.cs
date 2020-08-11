using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [System.Serializable]
    public struct QuestInfo
    {

        //public questType Type;
        //[System.Serializable]
        public enum questType
        {
            Clear, // 스테이지 클리어
            Spawn, // 유닛 소환
            Kill, // 적 처치
            Reinforce, // 강화
            SaveHP, // 석상 or 대천사 체력
            SaveUnit // 유닛 살려두기
        }

        public questType Type;

        public int QuestValue;

    }

    public static GameManager instance;

    [SerializeField]
    //List<QuestInfo> Quests = new List<QuestInfo>();
    QuestInfo[] Quests;

    public int fast;
    public Transform bossPos;
    public GameObject boss;
    [HideInInspector]
    public bool killed;
    [HideInInspector]
    public int CurrentMoney;
    [HideInInspector]
    public int starCt;

    public GameObject winObj;
    public GameObject loseObj;

    public Image[] starObj;
    public Sprite starSprite;
    public Text[] text_Quest;
    private List<int> star = new List<int>{0, 0, 0};
    private List<string> strings = new List<string> { "one", "two", "three" }; 
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



    void Awake()
    {
        instance = this;

        Time.timeScale = 1;
        starCt = PlayerPrefs.GetInt("saveStarCt");
        for (int i = 0; i < star.Count; i++)
        {
            star[i] = PlayerPrefs.GetInt(strings[i]);
            if(star[i] == 1)
            {
                starObj[i].sprite = starSprite;

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

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
            if (Quests[i].Type == QuestInfo.questType.Clear)
            {
                text_Quest[i].text = "스테이지 " + Quests[i].QuestValue + " 클리어 하기";
                if(cleared == true) { CheckStar(i); }
            }
            else if(Quests[i].Type == QuestInfo.questType.Spawn)
            {
                text_Quest[i].text = "천사 유닛 " + Quests[i].QuestValue + " 명 이상 소환하기";
                if (spawnCt > Quests[i].QuestValue) { CheckStar(i); }
            }
            else if (Quests[i].Type == QuestInfo.questType.Kill)
            {
                text_Quest[i].text = "악마 유닛 " + Quests[i].QuestValue + " 마리 처치하기";
                if (killCt > Quests[i].QuestValue) { CheckStar(i); }
            }
            else if (Quests[i].Type == QuestInfo.questType.Reinforce)
            {
                if (reinforceCt > Quests[i].QuestValue) { CheckStar(i); }
            }
            else if (Quests[i].Type == QuestInfo.questType.SaveHP)
            {
                if (saveHPCt > Quests[i].QuestValue) { CheckStar(i); }
            }
            else if (Quests[i].Type == QuestInfo.questType.SaveUnit)
            {
                if (saveUnitCt > Quests[i].QuestValue) { CheckStar(i); }
            }
        }
    }

    void CheckStar(int i)
    {
        if(star[i] == 0)
        {
            starCt++;
            star[i] = 1;
            PlayerPrefs.SetInt("saveInt", star[i]);
            PlayerPrefs.SetInt("saveStarCt", starCt);
            PlayerPrefs.SetInt(strings[i], starCt);
            starObj[i].sprite = starSprite;

        }
    }
}
