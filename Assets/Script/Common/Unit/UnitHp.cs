using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class UnitHp : MonoBehaviour
    {
        public int Hp;
        private bool Invincible = false;

        void Damaged(int damage)
        {
            if (!Invincible)
            Hp -= damage;

            if (Hp <= 0)
            {
                if (this.gameObject.layer == 10)
                {
                    UnitDeath();
                    
                }
                else if(this.gameObject.layer == 12)
                {
                    if (this.gameObject.tag == "Enemy")
                    {
                        GameManager.instance.SpawnBoss();
                    }
                    else
                    {
                        GameManager.instance.LoseScreen();
                    }
                }
                else if(this.gameObject.layer == 14)
                {
                    GameManager.instance.WinScreen();
                }
                GameManager.instance.killed = true;
                //this.gameObject.transform.position = new Vector3(60, transform.position.y, transform.position.z);
                //this.gameObject.tag = "Dead";
                Destroy(gameObject);
            }
        }

        protected virtual void UnitDeath()
        {
            //GameManager.instance.killCt++;
        }
        IEnumerator Protect(float t)
        {
            Debug.Log(t);
            Invincible = true;
            yield return new WaitForSeconds(t);
            Destroy(transform.GetChild(1).gameObject);
            Invincible = false;
        }
}
