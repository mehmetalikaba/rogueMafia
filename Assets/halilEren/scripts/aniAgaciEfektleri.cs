using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class aniAgaciEfektleri : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public aniAgaciEfektleri[] aniAgaci;
    public bool kilitAcik, baslangicSkill, acilabilir, acti, mouseUzerinde, mouseYok, kilitAcildi;
    Animator animator;
    public envanterKontrol envanterKontrol;
    public yetenekKontrol yetenekKontrol;
    public float gerekenAniPuani;
    private string actiKey, acilabilirKey, kilitKey;
    public Button buton;

    void Start()
    {
        animator = GetComponent<Animator>();
        envanterKontrol = FindAnyObjectByType<envanterKontrol>();
        yetenekKontrol = FindObjectOfType<yetenekKontrol>();

        actiKey = gameObject.name + "_acti";
        acti = PlayerPrefs.GetInt(actiKey, 0) == 1;


        acilabilirKey = gameObject.name + "_acilabilir";
        acilabilir = PlayerPrefs.GetInt(acilabilirKey, 0) == 1;

        kilitKey = gameObject.name + "_kilit";
        kilitAcik = PlayerPrefs.GetInt(kilitKey, 0) == 1;

        if (baslangicSkill)
            kilitAcik = true;

    }
    void Update()
    {
        if (mouseYok)
            animator.SetBool("highLightedBool", false);

        if (acti)
        {
            animator.SetBool("yanma", true);
            animator.SetBool("yanipSonme", false);
            animator.SetBool("highLightedBool", false);

            acilabilir = false;
            kilitAcik = false;
            baslangicSkill = false;
            mouseUzerinde = false;
            mouseYok = false;

            buton = GetComponent<Button>();
            buton.interactable = false;
        }
        else
        {
            if (kilitAcik)
            {
                kilitleriAc();
                if (envanterKontrol.aniPuani >= gerekenAniPuani && !acti)
                {
                    acilabilir = true;
                    animator.SetBool("highLightedBool", false);
                    animator.SetBool("sonme", false);
                    animator.SetBool("yanipSonme", true);

                    acilabilirKey = gameObject.name + "_acilabilir";
                    PlayerPrefs.SetInt(acilabilirKey, acilabilir ? 1 : 0);
                    PlayerPrefs.Save();
                }
                else if (envanterKontrol.aniPuani <= gerekenAniPuani && !acti)
                {
                    acilabilir = false;
                    animator.SetBool("yanipSonme", true);
                    animator.SetBool("sonme", false);
                }
                if (mouseUzerinde)
                {
                    animator.SetBool("highLightedBool", true);
                    animator.SetBool("sonme", false);
                    animator.SetBool("yanipSonme", false);
                }
            }
        }
    }

    public void gelistirilmis()
    {
        if (acilabilir)
        {
            acti = true;
            acilabilir = false;
            PlayerPrefs.SetInt(actiKey, acti ? 1 : 0);
            PlayerPrefs.Save();
            buton = GetComponent<Button>();
            buton.interactable = false;

            foreach (var ani in aniAgaci)
            {
                ani.kilitAcik = true;
            }
        }
    }

    public void kilitleriAc()
    {
        if (!kilitAcildi)
        {
            kilitAcildi = true;
            kilitKey = gameObject.name + "_kilit";
            PlayerPrefs.SetInt(kilitKey, kilitAcik ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseUzerinde = true;
        mouseYok = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseUzerinde = false;
        mouseYok = true;
    }
}
