using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reinforce_Grid : MonoBehaviour
{
    public int upPer;
    public enum Type
    {
        Unit,
        Skill
    }

    public Type type;
    public GameObject upObj;
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
        

        if (price[currTier] <= GameManager.instance.Gold)
        {
            GameManager.instance.Gold -= price[currTier];
            if (type == Type.Unit)
                upObj.GetComponent<Unit>().Upgrade(upPer);
            /*
            else
            {
                try
                {
                    upObj.GetComponent<EnemyDam>().Upgrade(upVal);
                }
                catch { }
            }
            */
            currTier++;

            if (price.Length - 1 < currTier)
            {
                txt.text = "Max";
                b.onClick.RemoveAllListeners();
                return;
            }
            Refresh();
        }

    }

    void Refresh()
    {
        txt.text = price[currTier].ToString();
        
    }
}
