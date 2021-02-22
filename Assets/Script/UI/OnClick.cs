using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour
{
    private int i = 0;
    bool isTrue_speed;
    public GameObject GridLayout;
    public GameObject image;
    private readonly int hashisEnter = Animator.StringToHash("isEnter");
    Animator animator;
    bool isTrue_reinforce;
    public GameObject[] hideObj;
    // ----------------------유지------------------------
    void Start()
    {
        try
        {
            animator = image.GetComponent<Animator>();
            animator.SetBool(hashisEnter, true);
            GridLayout.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
        i = SceneManager.GetActiveScene().buildIndex;

        
    }

    private void OnLevelWasLoaded(int level)
    {
        // animator = image.GetComponent<Animator>(); 분리 후 복구
        // animator.SetBool(hashisEnter, true);
        ClosePopUP();
        // ------------------분리-------------------------
        if (PlayerPrefs.GetInt("Next") == 1)
        {
            OpenPopUp(2);
            PlayerPrefs.SetInt("Next", 0);
        }
        // -------------------------------------------
    }

    public void LSAnimtaion()
    {
        animator.SetBool(hashisEnter, false);
    }

    public void Faster()
    {
        Debug.Log("Clicked");
        isTrue_speed = !isTrue_speed;
        if (isTrue_speed) { Time.timeScale = GameManager.instance.fast; }
        else { Time.timeScale = 1; }
    }

    public void OpenPopUp(int i)
    {
        transform.GetChild(i).gameObject.SetActive(true);
    }


    public void ClosePopUP()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "PopUp")
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    // ----------------------------------------------------
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


        if (i == s)
        {
            SceneManager.LoadScene("Loading");
        }
        // else { i = s; }
        i = s;

        SceneManager.LoadScene(s, LoadSceneMode.Single);
    }

    

    public void GoNext() // 다음 씬으로 이동
    {
        PlayerPrefs.SetInt("Next", 1);
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        LoadScene(2);
    }

    public void GoMain()
    {
        LoadScene(1);
    }

    public void GoAgain()
    {
        LoadScene(i);
    }


    public void HideObj(int i)
    {
        hideObj[i].SetActive(false);
    }
}
