using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour
{
    public GameObject[] popUp;
    private int i = 0;
    bool isTrue_speed;
    public GameObject GridLayout;
    bool isTrue_reinforce;

    void Start()
    {
        GridLayout.SetActive(false);
    }

    void Update()
    {

    }

    public void TryReinforce()
    {
        var name = EventSystem.current.currentSelectedGameObject.name;
        Text childTxt = GameObject.Find(name).transform.GetChild(0).transform.GetComponent<Text>();
        isTrue_reinforce = !isTrue_reinforce;
        if (isTrue_reinforce)
        {
            childTxt.text = "취소";
        }
        else { childTxt.text = "강화"; }
        GridLayout.SetActive(isTrue_reinforce);
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Quit() // 게임 종료
    {
        Application.Quit();
    }

    public void LoadSpecificScene(string scene) // 특정 씬 호출
    {
        /*
        i = int.Parse(this.gameObject.transform.GetChild(0).name);
        GameManager.instance.thisStage = i;
        Debug.Log(i);
        */
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void LoadScene(int s)
    {
        i = s;
        GameManager.instance.thisStage = i;
        SceneManager.LoadScene(s, LoadSceneMode.Single);
    }

    public void GoNext() // 다음 씬으로 이동
    {
        if (i < SceneManager.sceneCountInBuildSettings)
        {
            i++;
        }
        LoadScene(i);
    }

    public void GoFirst()
    {
        LoadScene(0);
    }

    public void GoAgain()
    {
        LoadScene(i);
    }


    public void Faster()
    {
        isTrue_speed = !isTrue_speed;
        if (isTrue_speed) { Time.timeScale = GameManager.instance.fast; }
        else { Time.timeScale = 1; }
    }

    public void OpenPopUp(int i)
    {
        popUp[i].SetActive(true);
    }

    public void ClosePopUP()
    {
        Debug.Log(this.gameObject);
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).tag == "PopUp")
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
