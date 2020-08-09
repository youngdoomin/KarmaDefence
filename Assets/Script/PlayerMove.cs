using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;
    public float PlayerClamp;
    public Transform rainTr;
    private float rainX = 10;
    private float rainY = 20;
    Rigidbody rb;
     void Start () {
         rb = GetComponent<Rigidbody>();
     }

    // Update is called once per frame
    void Update()
    {
        /*
        move = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
        if (transform.position.x < PlayerClamp && Input.GetAxis("Horizontal") > 0)
            transform.position += new Vector3(move, 0, 0);
        else if (transform.position.x > -PlayerClamp && Input.GetAxis("Horizontal") < 0)
            transform.position += new Vector3(move, 0, 0);
        */
        float mH = Input.GetAxis("Horizontal");
        
            rb.velocity = new Vector3(mH * MoveSpeed, 0, 0);

        Vector3 pos = rb.position;
        Vector3 local = transform.localScale;
        pos.x = Mathf.Clamp(pos.x, -PlayerClamp, PlayerClamp);
        rb.position = pos;

        if (mH < 0)
        {
            
            local.x = -1;
            rainTr.localPosition = new Vector3(-rainX, rainY, -transform.position.z);
        }
        else if(mH > 0)
        {
            local.x = 1;
            rainTr.localPosition = new Vector3(rainX, rainY, -transform.position.z);
        }
        transform.localScale = local;


    }
}
