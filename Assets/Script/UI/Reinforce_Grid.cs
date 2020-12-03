using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reinforce_Grid : MonoBehaviour
{
    public int[] price;
    Text txt;
    Button b;
    int currTier;

    void Start()
    {
        b = GetComponent<Button>();
        b.onClick.AddListener(() => Upgrade());
        txt = transform.GetChild(0).GetComponent<Text>();
        Refresh();
    }

    void Update()
    {
        
    }

    public void Upgrade()
    {
        if(price[currTier] <= GameManager.instance.Gold)
        {
            GameManager.instance.Gold -= price[currTier];
            currTier++;
            Refresh();
        }

    }

    void Refresh()
    {
        txt.text = price[currTier].ToString();
    } 
}
