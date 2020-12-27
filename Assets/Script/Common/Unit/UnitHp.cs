using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHp : MonoBehaviour
{
    public int Hp;
    private float initHp;

    private bool Invincible = false;

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
        hpBarOffset.y += 5;
    }

    public void Damaged(int damage)
    {

        if (Invincible == false)
            Hp -= damage;

        hpBarImage.fillAmount = Hp / initHp;

        if (Hp <= 0)
        {
            //hpBarImage.GetComponentsInParent<RawImage>()[0].color = Color.clear;
            //hpBarImage.transform.parent.GetComponent<RawImage>().color = Color.clear;
            Destroy(hpBarImage.transform.parent.gameObject);
            Disable();
        }
    }

    protected virtual void Disable()
    {
        Destroy(gameObject);
    }
    public IEnumerator Protect(float t)
    {
        Debug.Log(t);
        Invincible = true;
        yield return new WaitForSeconds(t);
        Destroy(transform.GetChild(1).gameObject);
        Invincible = false;
    }

    void SetHpBar(Canvas canvas)
    {
        GameObject hpBar;
        hpBar = Instantiate(hpBarPrefab, canvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[0];

        var _hpBar = hpBar.GetComponent<HpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;

    }

}
