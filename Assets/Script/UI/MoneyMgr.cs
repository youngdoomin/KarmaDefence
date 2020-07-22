using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyMgr : MonoBehaviour
{
    public Text MoneyText;

    public int MoneyCap;
    public float IncDelay;
    public int IncAmount;


    void Start()
    {
        //register new event to onclick with the variables that control your args
        MoneyText.text = "0 / " + MoneyCap;
        StartCoroutine(MoneyInc());
    }

    void Update()
    {
        MoneyText.text = GameManager.instance.CurrentMoney + "/" + MoneyCap;
    }




    protected IEnumerator MoneyInc()
    {
        if(GameManager.instance.CurrentMoney + IncAmount <= MoneyCap)
        {
            GameManager.instance.CurrentMoney += IncAmount;
        }
        else { GameManager.instance.CurrentMoney = MoneyCap; }
        yield return new WaitForSeconds(IncDelay);
        StartCoroutine(MoneyInc());
    }
}
