using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quest;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    

    public static GameManager instance;

    public StageQuest[] stQuest;
    // public QuestInfo[] Quests;
    // private List<bool> IsComplete = new List<bool> { false, false, false };
    [SerializeField]
    public QuestUI[] ui;
    public int fast;
    [HideInInspector]
    public bool killed, Invincible;
    [HideInInspector]
    public int CurrentMoney, Gold;
    [HideInInspector]
    public int starCt;
    [HideInInspector]
    public int stageStar;
    public Sprite starSprite;

    public GameObject winObj;
    public GameObject currStar;


    // private List<int> star = new List<int>{0, 0, 0};
    // private List<string> strings = new List<string> { "one", "two", "three" }; 
    [HideInInspector]
    public int spawnCt;
    [HideInInspector]
    public int killCt;
    [HideInInspector]
    public int killbySkillCt;
    [HideInInspector]
    public int killbySwordCt;
    [HideInInspector]
    public int killbyBowCt;
    [HideInInspector]
    public int bossHitbySkill;
    [HideInInspector]
    public int reinforceCt;
    [HideInInspector]
    public int saveHQCt;
    [HideInInspector]
    public int saveUnitCt;

    Reinforce_Grid[] rgs;

    [System.Serializable]
    public class StageQuest
    {
        public QuestInfo[] Quests;
    }
    void Awake()
    {
        instance = this;

        Time.timeScale = 1;
        
        starCt = PlayerPrefs.GetInt("saveStarCt");

        if(SceneManager.GetActiveScene().name == "Stage_Scene")
        {
            /*
            Debug.Log("star active");
            stageStar = PlayerPrefs.GetInt("stageStarCt");
            for (int i = 0; i < star.Count; i++)
            {
                star[i] = PlayerPrefs.GetInt(strings[i]);
                if(star[i] == 1)
                {
                    ui[i].starObj.sprite = starSprite;

                }
            }
            */
        }
        else
        {
            currStar.GetComponent<Text>().text = PlayerPrefs.GetInt("saveStarCt").ToString();
            rgs = (Reinforce_Grid[])GameObject.FindObjectsOfType(typeof(Reinforce_Grid));
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

    
    public void Result()
    {
        winObj.SetActive(true);

        StageQuest q = stQuest[PlayerPrefs.GetInt("level") - 1];
        for (int i = 0; i < q.Quests.Length; i++)
        {
            if (q.Quests[i].Type == QuestInfo.questType.Clear)
            {
                ui[i].text_Quest.text = "스테이지 " + q.Quests[i].QuestValue + " 클리어 하기";
                CheckStar(i);
            }
            else if(q.Quests[i].Type == QuestInfo.questType.Spawn)
            {
                ui[i].text_Quest.text = "천사 유닛 " + q.Quests[i].QuestValue + " 명 이상 소환하기";
                if (spawnCt > q.Quests[i].QuestValue) { CheckStar(i); }
            }
            else if (q.Quests[i].Type == QuestInfo.questType.Kill)
            {
                ui[i].text_Quest.text = "악마 유닛 " + q.Quests[i].QuestValue + " 마리 처치하기";
                if (killCt > q.Quests[i].QuestValue) { CheckStar(i); }
            }
            else if (q.Quests[i].Type == QuestInfo.questType.KillbySkill)
            {
                ui[i].text_Quest.text = "대 천사 스킬로 악마 유닛 " + q.Quests[i].QuestValue + " 마리 처치하기";
                // if (killCt > Quests[i].QuestValue) { CheckStar(i); } // 대 천사 스킬 킬카운트 변수 생성 필요
            }
            else if (q.Quests[i].Type == QuestInfo.questType.KillbySword)
            {
                ui[i].text_Quest.text = "대 천사 스킬로 악마 유닛 " + q.Quests[i].QuestValue + " 마리 처치하기";
                // if (killCt > Quests[i].QuestValue) { CheckStar(i); } // 대 천사 스킬 킬카운트 변수 생성 필요
            }
            else if (q.Quests[i].Type == QuestInfo.questType.KillbyBow)
            {
                ui[i].text_Quest.text = "대 천사 스킬로 악마 유닛 " + q.Quests[i].QuestValue + " 마리 처치하기";
                // if (killCt > Quests[i].QuestValue) { CheckStar(i); } // 대 천사 스킬 킬카운트 변수 생성 필요
            }
            else if (q.Quests[i].Type == QuestInfo.questType.BossHitbySkill)
            {
                ui[i].text_Quest.text = "궁수 유닛으로 악마 유닛 " + q.Quests[i].QuestValue + " 마리 처치하기";
                // if (killCt > Quests[i].QuestValue) { CheckStar(i); } // 대 천사 스킬 킬카운트 변수 생성 필요
            }
            else if (q.Quests[i].Type == QuestInfo.questType.Reinforce)
            {
                if (reinforceCt > q.Quests[i].QuestValue) { CheckStar(i); }
            }
            else if (q.Quests[i].Type == QuestInfo.questType.SaveHP)
            {
                if (saveHQCt > q.Quests[i].QuestValue) { CheckStar(i); }
            }
            else if (q.Quests[i].Type == QuestInfo.questType.SaveUnit)
            {
                if (saveUnitCt > q.Quests[i].QuestValue) { CheckStar(i); }
            }
        }
        
    }
    
    void CheckStar(int i)
    {
        int lv = PlayerPrefs.GetInt("level");
        // Get boolean using PlayerPrefs
        var isDuplicate = PlayerPrefs.GetInt(lv + i.ToString()) == 1 ? true : false;
        if (!isDuplicate)
        {
            isDuplicate = true;
            PlayerPrefs.SetInt(lv + i.ToString(), isDuplicate ? 1 : 0);
            starCt++;
            Debug.Log(starCt);
            PlayerPrefs.SetInt("saveStarCt", starCt);
        }
        ui[i].starObj.sprite = starSprite;

        Debug.Log(lv + i.ToString());
        /*
        if(star[i] == 0)
        {
            
            stageStar++;
            star[i] = 1;
            PlayerPrefs.SetInt("saveInt", star[i]);
            PlayerPrefs.SetInt("saveStarCt", stageStar);
            PlayerPrefs.SetInt(strings[i], stageStar);
            IsComplete[i] = true;
            ui[i].starObj.sprite = starSprite;

        }
        */
    }

    public void TestStar()
    {
        starCt++;
        PlayerPrefs.SetInt("saveStarCt", starCt);
        currStar.GetComponent<Text>().text = PlayerPrefs.GetInt("saveStarCt").ToString();
    }
    
    public void RefreshStar()
    {
        // PlayerPrefs.SetInt("saveStarCt", starCt);
        currStar.GetComponent<Text>().text = starCt.ToString();
    }

    public void CancelUpgrade() 
    {
        PlayerPrefs.SetInt("saveStarCt", starCt);
        currStar.GetComponent<Text>().text = PlayerPrefs.GetInt("saveStarCt").ToString();
        foreach (Reinforce_Grid rg in rgs)
        {
            rg.currTier = PlayerPrefs.GetInt(rg.gameObject.name);
            rg.Refresh();
        }
    }

    public void SaveUpgrade()
    {
        foreach (Reinforce_Grid rg in rgs)
        {
            rg.Save();
        }
        starCt = int.Parse(FindObjectOfType<Reinforce_Grid>().starTxt.text);
        PlayerPrefs.SetInt("saveStarCt", starCt);
        currStar.GetComponent<Text>().text = PlayerPrefs.GetInt("saveStarCt").ToString();
    }

}
