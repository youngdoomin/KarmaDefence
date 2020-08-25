using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quest;


public class ShowQuest : MonoBehaviour
{
    [SerializeField]
    QuestInfo[] quests;
    [SerializeField]
    QuestUI[] ui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableQuest()
    {
        quests = GameManager.instance.Quests;
    }
}
