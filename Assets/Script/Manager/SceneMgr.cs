using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    private int i = 0;

    public GameObject[] hideObj;
    public GameObject tutoPopUpObj;

    // Start is called before the first frame update
    void Start()
    {
        i = SceneManager.GetActiveScene().buildIndex;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            if (PlayerPrefs.GetInt("Level 1_Star") == 0)
            {
                GameManager.instance.ToggleTime();
                tutoPopUpObj.SetActive(true);
            }
            else if(PlayerPrefs.GetInt("Next") == 1)
            {
                this.GetComponent<OnClick>().OpenPopUp(2);
                HideObj(0);
                PlayerPrefs.SetInt("Next", 0);
                Debug.Log("next");
            }
        }
        else
        {
            this.GetComponent<OnClick>().ClosePopUP();
            BackAll();

            
            
            
        }
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
        this.GetComponent<OnClick>().ClosePopUP();
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

    public void BackAll()
    {
        for (int i = 0; i < hideObj.Length; i++)
        {
            hideObj[i].SetActive(true);
        }
    }
}
