using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoldMgr : MonoBehaviour
{
    public Text GoldText;
    private int goldCap = 999;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.Gold > goldCap)
        {
            GameManager.instance.Gold = goldCap;
        }

        GoldText.text = GameManager.instance.Gold + "/" + goldCap;
    }
}
