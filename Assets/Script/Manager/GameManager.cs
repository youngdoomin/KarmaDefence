using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quest;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    

    public static GameManager instance;

    [HideInInspector]
    //List<QuestInfo> Quests = new List<QuestInfo>();
    public QuestInfo[] Quests;
    private List<bool> IsComplete = new List<bool> { false, false, false };
    [SerializeField]
    public QuestUI[] ui;
    public int fast;
    [HideInInspector]
    public bool killed, Invincible;
    [HideInInspector]
    public int CurrentMoney, Gold;
    [HideInInspector]
    public int starCt;
    public Sprite starSprite;

    public GameObject winObj;
    public GameObject currStar;


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
        
        if(SceneManager.GetActiveScene().name == "Stage_Scene")
        {
            Debug.Log("star active");
            starCt = PlayerPrefs.GetInt("saveStarCt");
            for (int i = 0; i < star.Count; i++)
            {
                star[i] = PlayerPrefs.GetInt(strings[i]);
                if(star[i] == 1)
                {
                    ui[i].starObj.sprite = starSprite;

                }
            }

        }
        else
            currStar.GetComponent<Text>().text = PlayerPrefs.GetInt("saveStarCt").ToString();
        

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
        cleared = true;

        for (int i = 0; i < Quests.Length; i++)
        {
            if (Quests[i].Type == QuestInfo.questType.Clear)
            {
                ui[i].text_Quest.text = "스테이지 " + Quests[i].QuestValue + " 클리어 하기";
                if(cleared == true) { CheckStar(i); }
            }
            else if(Quests[i].Type == QuestInfo.questType.Spawn)
            {
                ui[i].text_Quest.text = "천사 유닛 " + Quests[i].QuestValue + " 명 이상 소환하기";
                if (spawnCt > Quests[i].QuestValue) { CheckStar(i); }
            }
            else if (Quests[i].Type == QuestInfo.questType.Kill)
            {
                ui[i].text_Quest.text = "악마 유닛 " + Quests[i].QuestValue + " 마리 처치하기";
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
            IsComplete[i] = true;
            ui[i].starObj.sprite = starSprite;

        }
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
        Reinforce_Grid[] rgs = (Reinforce_Grid[])GameObject.FindObjectsOfType(typeof(Reinforce_Grid));
        foreach (Reinforce_Grid rg in rgs)
        {
            rg.currTier = PlayerPrefs.GetInt(rg.gameObject.name);
            rg.Refresh();
        }
    }

    public void SaveUpgrade()
    {

    }

}
