using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHp : MonoBehaviour
{
    public int Hp;
    private bool Invincible = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Damaged(int damage)
    {
        if (!Invincible)
        Hp -= damage;

        if (Hp <= 0)
        {
            if(this.gameObject.layer == 8)
            {
                Debug.Log("Hq destroyed");
            }
            GameManager.instance.killed = true;
            this.gameObject.transform.position = new Vector3(60, transform.position.y, transform.position.z);
            //this.gameObject.tag = "Dead";
            Destroy(gameObject);
        }
    }

    IEnumerator Protect(float t)
    {
        Invincible = true;
        yield return new WaitForSeconds(t);
        Destroy(transform.GetChild(1).gameObject);
        Invincible = false;
    }
}
