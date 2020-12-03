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
                Disable();
            }
        }

        protected virtual void Disable()
        {
            Destroy(gameObject);
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
