using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCt : MonoBehaviour
{
    Text txt;
    // Start is called before the first frame update
    void Start()
    {
        //txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Price(int Amt)
    {
        txt = GetComponent<Text>();
        txt.text = Amt.ToString();
    }
}
