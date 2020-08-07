using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public float changeTime;
    public Sprite[] idleSprite;
    public Sprite[] walkSprite;
    public Sprite[] attackSprite;
    private float checkTime;
    private SpriteRenderer spriteRender;
    private int i = 0;

    [HideInInspector]
    public string functionName = "Idle";
    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = idleSprite[i];
        
    }

    // Update is called once per frame
    void Update()
    {
        checkTime += Time.deltaTime;
        if(checkTime >= changeTime)
        {
            ChangeSprite(functionName);
            
        }
    }

    void ChangeSprite(string str)
    {
       
        if (i >= idleSprite.Length - 1)
        {
            i = 0;
        }
        else
        {
            i++;

        }
        spriteRender.sprite = idleSprite[i];
        checkTime = 0;
    }
}
