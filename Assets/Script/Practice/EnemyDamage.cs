using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    private const string bulletTag = "BULLET";
    //생명 게이지
    private float hp = 100.0f;
    //초기 생명 수치
    private float initHp = 100.0f;


    //생명 게이지 프리팹을 저장할 변수
    public GameObject hpBarPrefab;
    //public GameObject hpBarOverPb;
    //생명 게이지의 위치를 보정할 오프셋
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    //부모가 될 Canvas 객체
    private Canvas uiCanvas;
    //private Canvas overlayCanvas;
    //생명 수치에 따라 fillAmount 속성을 변경할 Image
    private Image hpBarImage;

    public Image worldSlider;
    public Text damageCt;


    void Start()
    {
        //생명 게이지의 생성 및 초기화
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        //overlayCanvas = GameObject.Find("UICanvas_Overlay").GetComponent<Canvas>();

        SetHpBar(uiCanvas);
        hpBarOffset.y += 5;
    }

    void SetHpBar(Canvas canvas)
    {
        GameObject hpBar;
        hpBar = Instantiate<GameObject>(hpBarPrefab, canvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var _hpBar = hpBar.GetComponent<EnemyHpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;

    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == bulletTag)
        {
            coll.gameObject.SetActive(false);

            //생명 게이지 차감
            //hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;
            //생명 게이지의 fillAmount 속성을 변경
            var current_hp = hp / initHp;

            hpBarImage.fillAmount = current_hp;

            if (hp <= 0.0f)
            {
                
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
            }
        }
    }

}
