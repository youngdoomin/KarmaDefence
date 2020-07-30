using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuySomething : MonoBehaviour
{
    //your button
    public Button b;

    //control the value to pass to event as you need
    public int Amt;
    public GameObject UnitObj;
    public string FindObj;
    public string Message;
    void Start()
    {
        //register new event to onclick with the variables that control your args
        b.onClick.AddListener(() => BuySome(Amt, UnitObj));
        this.gameObject.BroadcastMessage("Price", Amt);
    }


    public void BuySome(int Amt, GameObject obj)
    {
        if (GameManager.instance.CurrentMoney >= Amt)
        {

            GameManager.instance.CurrentMoney -= Amt;
            var spawn = GameObject.Find(FindObj);
            spawn.SendMessage(Message, obj);
        }
    }
}