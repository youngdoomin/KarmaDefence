using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    public float Min;
    public float Max;
    public float ChangeT;
    public Renderer renderer;
    //private const float num = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Blink());
    }
    

    IEnumerator Blink()
    {
        float num = 0.1f;
        var color = renderer.material.color;
        while(true)
        {
            color.a += num;
            renderer.material.color = color;
            if(color.a >= Max || color.a <= Min) { num = -num; }
            yield return new WaitForSeconds(ChangeT); 
        }
    }
}
