using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;
    public float PlayerClamp;
    public Transform rainTr;
    private float rainX = 10;
    private float rainY = 0;
    Rigidbody rb;
     void Start () {
         rb = GetComponent<Rigidbody>();
         rainTr.localPosition = new Vector3(rainX, rainY, -transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        float mH = Input.GetAxis("Horizontal");
        
            rb.velocity = new Vector3(mH * MoveSpeed, 0, 0);

        Vector3 pos = rb.position;
        Vector3 local = transform.localScale;
        pos.x = Mathf.Clamp(pos.x, -PlayerClamp, PlayerClamp);
        rb.position = pos;

        if (mH < 0)
        {
            
            local.x = -1;
            rainTr.localPosition = new Vector3(-rainX, rainY, 0);
        }
        else if(mH > 0)
        {
            local.x = 1;
            rainTr.localPosition = new Vector3(rainX, rainY, 0);
        }

        transform.localScale = local;

    }
}
