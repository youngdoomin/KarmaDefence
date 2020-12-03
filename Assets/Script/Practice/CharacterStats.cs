using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currHealth { get; private set; }

    public Stat damage;
    public Stat armor;

    private void Awake()
    {
        currHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage (int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currHealth -= damage;
        Debug.Log(transform.name + "takes " + damage + "damage.");

        if(currHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // 이 메소드는 덮어쓰기 가능
        Debug.Log(transform.name + " died.");
    }
}
