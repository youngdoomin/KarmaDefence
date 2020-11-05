using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int Damage;
    public float AttackRange;  // 공격 사거리
    public float Speed;
    public float AttackSpeed;
    public float AttackDelay;    // 원거리 공격 딜레이
    
    public GameObject Projectile;   // 원거리 투사체
    public Transform ShootPos;  // 발사 위치

    private bool Fighting;  // 근접 공격 상태
    private bool Shooting;  // 원거리 공격 상태

    private GameObject[] Other; // 아군, 적 감지
    private string OtherTag;
    Rigidbody rb;

    Animator animator;
    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashWalk = Animator.StringToHash("Walk");
    private readonly int hashSpeed = Animator.StringToHash("Speed");
    private float speed_animation = 1f;
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
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
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

            /*
            if(GameManager.instance.killed == true)
            {
                GameManager.instance.killed = false;
            }
                */

            foreach (GameObject potentTarget in allEn) // 가장 가까운 적 공격
            {
                float distTarget = potentTarget.transform.position.x - this.transform.position.x;
                if (distTarget < closestDist && potentTarget.layer != 9
                && ((this.gameObject.tag == "Friendly" && potentTarget.transform.position.x > this.transform.position.x) || (this.gameObject.tag == "Enemy" && potentTarget.transform.position.x < this.transform.position.x))) // 적이 뒤로 넘어가면 공격 안함
                {
                    closestDist = distTarget;
                    closestEn = potentTarget;
                    //transform.LookAt(closestEn.transform);
                }
            }
            if (Class == Type.Shooter && AttackRange + closestEn.transform.localScale.x > closestDist && !Shooting) { StartCoroutine(Shoot(closestEn.transform)); } //&& FightDis < closestDist) 
            else if (Class == Type.Fighter && AttackRange + closestEn.transform.localScale.x > closestDist && !Fighting) { StartCoroutine(Fight(closestEn)); }
            else if (!Shooting && !Fighting)
            { 
                rb.velocity = transform.right * Speed;
                animator.SetBool(hashWalk, true);
            }

        }
        
    }

    IEnumerator Shoot(Transform Enemy)
    {
        animator.SetBool(hashWalk, false);
        animator.SetBool(hashAttack, true);

        animator.SetFloat(hashSpeed, speed_animation / AttackSpeed);
        rb.velocity = Vector3.zero;
        Shooting = true;
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(AttackSpeed);
        if(Enemy.gameObject != null)
        {
            ShootPos.LookAt(Enemy);
            GameObject projectile = Instantiate(Projectile, ShootPos.position, ShootPos.rotation);
            projectile.tag = OtherTag;
            projectile.name = Damage.ToString();
            animator.SetBool(hashAttack, false);

        }
        yield return new WaitForSeconds(AttackDelay);
        Shooting = false;
        transform.GetChild(1).gameObject.SetActive(false);
    }

    IEnumerator Fight(GameObject Enemy)
    {
        animator.SetBool(hashWalk, false);
        animator.SetBool(hashAttack, true);

        animator.SetFloat(hashSpeed, speed_animation / AttackSpeed);
        rb.velocity = Vector3.zero;
        Fighting = true;
        yield return new WaitForSeconds(AttackSpeed);
        if (Enemy != null) { Enemy.SendMessage("Damaged", Damage); }
        animator.SetBool(hashAttack, false);
        yield return new WaitForSeconds(AttackDelay);
        Fighting = false;
    }



}
