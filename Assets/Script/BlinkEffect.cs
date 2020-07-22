using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    public float Min;
    public float Max;
    public float ChangeT;
    public Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Blink());
    }
    //

    IEnumerator Blink()
    {
        var color = renderer.material.color;
        var num = 0.1f;
        while(true)
        {
            color.a += num;
            renderer.material.color = color;
            yield return new WaitForSeconds(ChangeT); 
            if(color.a >= Max || color.a <= Min) { num = -num; }
        }
    }
}
