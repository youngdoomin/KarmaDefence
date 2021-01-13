using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    public int totalLevel = 0;
    public int unlockedLevel = 1;
    private LevelButton[] levelButtons;
    private int totalPage = 0; 
    private int page = 0; // 페이지
    private int pageItem = 9; // 페이지 당 스테이지 개수
    // public GameObject nextButton;
    // public GameObject backButton;

    private void OnEnable()
    {
        levelButtons = GetComponentsInChildren<LevelButton>();
    }

    private void Start()
    {
        Refresh();
    }

    public void StartLevel(int level)
    {
        if (level == unlockedLevel)
        {
            unlockedLevel += 1;
            Refresh();
        }
    }
    /*
    public void ClickNext()
    {
        page += 1;
        Refresh();
    }
    
    public void ClickBack()
    {
        page -= 1;
        Refresh();
    }
    */
    public void Refresh()
    {
        totalPage = totalLevel / pageItem;

        int idx = page * pageItem;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = idx + i + 1;

            if (level <= totalLevel)
            {
                levelButtons[i].gameObject.SetActive(true);
                levelButtons[i].Setup(level, level <= unlockedLevel);

            }
            else
                levelButtons[i].gameObject.SetActive(false);
        }

        // CheckButton();
    }
    /*
    private void CheckButton()
    {
        backButton.SetActive(page > 0);
        nextButton.SetActive(page < totalPage);
    }
    */
}
