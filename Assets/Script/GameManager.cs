using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Awake()
    {
        instance = this;
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
}
