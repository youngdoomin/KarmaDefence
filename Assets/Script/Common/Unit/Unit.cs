using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int Damage;
    public float AttackRange;  // 공격 사거리
    public float Speed;
    public float AttackSpeed;

    public GameObject Effect;

    public GameObject Projectile;   // 원거리 투사체
    public Transform ShootPos;  // 발사 위치

    private bool Fighting;  // 근접 공격 상태
    private bool Shooting;  // 원거리 공격 상태

    private GameObject[] Other; // 아군, 적 감지
    private string OtherTag;
    //Rigidbody rb;

    Animator animator;
    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashWalk = Animator.StringToHash("Walk");
    private float speed_animation = 1f;

    bool isTrue;

    [System.Serializable]
    public enum Type
    {
        Shooter,
        Fighter
    }

    public Type Class;
    void Start()
    {
        
        FindObj();
        //rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (transform.childCount > 1)
            transform.GetChild(1).gameObject.SetActive(false);
    }

    void FindObj()
    {

        if (this.gameObject.tag == "Friendly" && this.gameObject.layer == 13)   // 상대방 구분
        { OtherTag = "Enemy"; }
        else
        { OtherTag = "Friendly"; }
        Other = GameObject.FindGameObjectsWithTag(OtherTag);
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            float closestDist = Mathf.Infinity;
            GameObject closestEn = null;
            GameObject[] allEn = Other;

            FindObj();

            foreach (GameObject potentTarget in allEn) // 가장 가까운 적 공격
            {
                float distTarget;// = Mathf.Abs(potentTarget.transform.position.x - transform.position.x);
                                 //float distTarget = (potentTarget.transform.position - this.transform.position).sqrMagnitude;

                try
                {
                    distTarget = Mathf.Abs(potentTarget.transform.position.x - transform.position.x);
                }
                catch (MissingReferenceException)
                {
                    //FindObj();
                    return;
                }

                if (distTarget < closestDist && potentTarget.layer != 9
                && ((this.gameObject.tag == "Friendly" && potentTarget.transform.position.x > this.transform.position.x) || (this.gameObject.tag == "Enemy" && potentTarget.transform.position.x < this.transform.position.x))) // 적이 뒤로 넘어가면 공격 안함
                {
                    closestDist = distTarget;
                    closestEn = potentTarget;
                }
            }
            if (Class == Type.Shooter && AttackRange + closestEn.transform.localScale.x > closestDist && !Shooting) { StartCoroutine(Shoot(closestEn)); } //&& FightDis < closestDist) 
            else if (Class == Type.Fighter && AttackRange + closestEn.transform.localScale.x > closestDist && !Fighting) { StartCoroutine(Fight(closestEn)); }
            else if (!Shooting && !Fighting)
            {
                Move();
            }

        }

    }

    void Move()
    {
        //rb.velocity = transform.right * Speed;
        
        if (this.gameObject.tag == "Friendly")
            transform.Translate(transform.right * Speed * Time.deltaTime);
        else
            transform.Translate(-transform.right * Speed * Time.deltaTime);
            
        animator.SetBool(hashWalk, true);
    }

    IEnumerator Shoot(GameObject Enemy)
    {
        Debug.Log("try Shoot");
        animator.SetBool(hashWalk, false);
        animator.SetBool(hashAttack, true);

        EffectToggle();

        animator.speed = speed_animation / AttackSpeed;
        //rb.velocity = Vector3.zero;
        Shooting = true;

        ShootPos.LookAt(Enemy.transform);
        if (Enemy != null)
        {
            yield return new WaitForSeconds(AttackSpeed);

            GameObject projectile = Instantiate(Projectile, ShootPos.position, ShootPos.rotation);
            projectile.tag = OtherTag;
            projectile.name = Damage.ToString();
            animator.SetBool(hashAttack, false);
            yield return new WaitForSeconds(AttackSpeed);
            animator.speed = 1;

        }
        Shooting = false;

        EffectToggle();
    }

    IEnumerator Fight(GameObject Enemy)
    {
        animator.SetBool(hashWalk, false);
        animator.SetBool(hashAttack, true);

        animator.speed = speed_animation / AttackSpeed / 2;
        //rb.velocity = Vector3.zero;
        Fighting = true;
        yield return new WaitForSeconds(AttackSpeed);
        if (Enemy != null)
        {
            Enemy.GetComponent<UnitHp>().Damaged(Damage);
            ShowEffect(Enemy);
            animator.SetBool(hashAttack, false);
            yield return new WaitForSeconds(AttackSpeed);
            animator.speed = 1;
        }
        Fighting = false;
    }

    void ShowEffect(GameObject Enemy)
    {
        GameObject effect = Instantiate(Effect);
        effect.transform.localScale = new Vector3(2, 2, 2);
        effect.transform.position = Enemy.transform.position;
        Destroy(effect, 1);
    }

    void EffectToggle()
    {
        if (transform.childCount > 1)
        {
            isTrue = !isTrue;
            transform.GetChild(1).gameObject.SetActive(isTrue);

        }
    }

    public void Upgrade(int per)
    {
        Debug.Log("Upgrade");
        Damage += Damage * per / 100;
        Debug.Log(Damage);
        //AttackSpeed -= AttackSpeed * per / 100;
        Speed += Speed * per / 100;
    }
    /*
    void Reset()
    {

    }
    */
}
