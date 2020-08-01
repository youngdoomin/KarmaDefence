using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float Speed;
    public float ShootDis;  // 원거리 공격 사거리
    public float ShootDelay;    // 원거리 공격 딜레이
    public GameObject Projectile;   // 원거리 투사체
    public Transform ShootPos;  // 발사 위치

    public int MeleeDam;
    public float FightDis;  // 근접 공격 거리
    public float FightDelay;    // 근접 공격 사거리

    private bool Fighting;  // 근접 공격 상태
    private bool Shooting;  // 원거리 공격 상태

    private GameObject[] Other; // 아군, 적 감지
    private string OtherTag;
    Rigidbody rb;

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
        if (ShootDis < FightDis)
            Debug.Log(this.gameObject.name + "거리 수치에 오류 발생");
        rb = GetComponent<Rigidbody>();


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
            float distTarget = (potentTarget.transform.position - this.transform.position).sqrMagnitude;
            if (distTarget < closestDist //&& potentTarget.layer != 9
            && ((this.gameObject.tag == "Friendly" && potentTarget.transform.position.x > this.transform.position.x) || (this.gameObject.tag == "Enemy" && potentTarget.transform.position.x < this.transform.position.x))) // 적이 뒤로 넘어가면 공격 안함
            {
                closestDist = distTarget;
                closestEn = potentTarget;
                //transform.LookAt(closestEn.transform);
            }
        }
        if (Class == Type.Shooter && ShootDis + closestEn.transform.localScale.x > closestDist && !Shooting) { StartCoroutine(Shoot()); } //&& FightDis < closestDist) 
        else if (FightDis + closestEn.transform.localScale.x > closestDist && !Fighting) { StartCoroutine(Fight(closestEn)); }
        else if (!Shooting && !Fighting)
        { rb.velocity = transform.forward * Speed; }
    }

    IEnumerator Shoot()
    {
        rb.velocity = Vector3.zero;
        GameObject projectile = Instantiate(Projectile, ShootPos.position, ShootPos.rotation);
        projectile.tag = OtherTag;
        Shooting = true;
        yield return new WaitForSeconds(ShootDelay);
        Shooting = false;
    }

    IEnumerator Fight(GameObject Enemy)
    {
        rb.velocity = Vector3.zero;
        Debug.Log("Fighting");
        Fighting = true;

        yield return new WaitForSeconds(FightDelay);
        if(Enemy != null) { Enemy.SendMessage("Damaged", MeleeDam); }
        
        Fighting = false;
    }

}
