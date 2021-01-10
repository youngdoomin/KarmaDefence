using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHp : MonoBehaviour
{
    public int Hp;
    [HideInInspector]
    public float initHp;


    //생명 게이지 프리팹을 저장할 변수
    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    //부모가 될 Canvas 객체
    private Canvas uiCanvas;
    //생명 수치에 따라 fillAmount 속성을 변경할 Image
    private Image hpBarImage;

    void Start()
    {
        initHp = Hp;
        //생명 게이지의 생성 및 초기화
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        //overlayCanvas = GameObject.Find("UICanvas_Overlay").GetComponent<Canvas>();

        SetHpBar(uiCanvas);
    }
    private void OnEnable()
    {
        if(uiCanvas != null)
            RestartHpBar(uiCanvas);
    }

    public void Damaged(int damage)
    {

        if (GameManager.instance.Invincible == false || this.gameObject.tag == "Enemy")
            Hp -= damage;

        hpBarImage.fillAmount = Hp / initHp;

        if (Hp <= 0)
        {
            //hpBarImage.GetComponentsInParent<RawImage>()[0].color = Color.clear;
            //hpBarImage.transform.parent.GetComponent<RawImage>().color = Color.clear;
            hpBarImage.transform.parent.gameObject.SetActive(false);
            Disable();
        }
    }

    protected virtual void Disable()
    {
        Hp = (int)initHp;
        gameObject.SetActive(false);
    }

    void SetHpBar(Canvas canvas)
    {
        GameObject hpBar;
        hpBar = Instantiate(hpBarPrefab, canvas.transform);

        HpFollow(hpBar);
    }

    void RestartHpBar(Canvas canvas)
    {
        GameObject hpBar = hpBarPrefab;
        for (int i = 0; i < canvas.transform.childCount - 1; i++)
        {
            if (!canvas.transform.GetChild(i).gameObject.activeInHierarchy && canvas.transform.GetChild(i).gameObject.name.Substring(0, 1) == this.gameObject.tag.Substring(0, 1))
            {
                hpBar = canvas.transform.GetChild(i).gameObject;
                hpBarImage.fillAmount = 1;
                hpBar.SetActive(true);
                break;
            }
            else if (i + 1 >= canvas.transform.childCount - 1)
            {
                hpBar = Instantiate(hpBarPrefab, canvas.transform);

            }
        }
        HpFollow(hpBar);
    }

    void HpFollow(GameObject obj)
    {
        hpBarImage = obj.GetComponentsInChildren<Image>()[0];

        var _hpBar = obj.GetComponent<HpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;

    }

}
