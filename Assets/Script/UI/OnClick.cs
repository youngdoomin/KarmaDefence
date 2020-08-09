using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    public GameObject[] popUp;
    public int fast;
    public static int i = 0;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(i < SceneManager.sceneCountInBuildSettings)
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
        Time.timeScale = fast;
    }

    public void OpenPopUp(int i)
    {
        popUp[i].SetActive(true);
    }
}
