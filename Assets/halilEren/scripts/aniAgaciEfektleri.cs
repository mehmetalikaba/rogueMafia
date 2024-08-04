using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class aniAgaciEfektleri : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;
    Button buton;
    bool mouseUzerinde, mouseYok;

    public bool kilitAcik, aniPuaniYeterli, yetenekGelistirildi;

    public shifuPanelScripti shifuPanelScripti;
    public envanterKontrol envanterKontrol;
    public yetenekObjesi yetenekObjesi;


    public int kacinciYetenek;
    public string hangiYetenek;

    void Start()
    {
        animator = GetComponent<Animator>();
        envanterKontrol = FindAnyObjectByType<envanterKontrol>();
        shifuPanelScripti = FindObjectOfType<shifuPanelScripti>();

        if (yetenekObjesi != null)
        {
            if (!kilitAcik && yetenekObjesi.yetenekSeviyesi != yetenekObjesi.maxSeviye)
            {
                foreach (var oncekiYetenekler in yetenekObjesi.gerekliYetenekler)
                {
                    if (oncekiYetenekler.yetenekSeviyesi > 0)
                        kilitAcik = true;
                }
            }
        }

        else
        {
            // baslangic skillidir ve kilit zaten aciktir // en yuksek seviyededir
        }
    }

    void Update()
    {
        if (kilitAcik)
        {
            if (yetenekObjesi.yetenekSeviyesi == yetenekObjesi.maxSeviye)
            {
                animator.SetBool("gelistirildi", true);
                animator.SetBool("sonme", false);
                animator.SetBool("mouseUzerinde", false);
                animator.SetBool("yanipSonme", false);

                mouseUzerinde = false;
                mouseYok = false;
                kilitAcik = false;
                aniPuaniYeterli = false;
                yetenekGelistirildi = true;

                buton = GetComponent<Button>();
                buton.interactable = false;
            }
            else
            {
                if (mouseYok)
                {
                    if (envanterKontrol.aniPuani >= yetenekObjesi.gerekliAniPuani)
                    {
                        aniPuaniYeterli = true;
                        animator.SetBool("gelistirildi", false);
                        animator.SetBool("sonme", false);
                        animator.SetBool("mouseUzerinde", false);
                        animator.SetBool("yanipSonme", true);
                    }
                    else if (envanterKontrol.aniPuani <= yetenekObjesi.gerekliAniPuani)
                    {
                        aniPuaniYeterli = false;
                        animator.SetBool("gelistirildi", false);
                        animator.SetBool("sonme", true);
                        animator.SetBool("mouseUzerinde", false);
                        animator.SetBool("yanipSonme", false);
                    }
                }
                else if (mouseUzerinde)
                {
                    animator.SetBool("gelistirildi", false);
                    animator.SetBool("sonme", false);
                    animator.SetBool("mouseUzerinde", true);
                    animator.SetBool("yanipSonme", false);
                }
            }
        }
        else
        {
            animator.SetBool("gelistirildi", false);
            animator.SetBool("sonme", true);
            animator.SetBool("mouseUzerinde", false);
            animator.SetBool("yanipSonme", false);
        }
    }

    public void yetenegiGelistir()
    {
        if (aniPuaniYeterli)
        {
            aniPuaniYeterli = false;
            kilitAcik = false;
            mouseUzerinde = false;
            mouseYok = false;
            yetenekGelistirildi = true;

            envanterKontrol.aniPuani -= yetenekObjesi.gerekliAniPuani;

            buton = GetComponent<Button>();
            buton.interactable = false;

            shifuPanelScripti.yetenekButonunaBasti(kacinciYetenek, hangiYetenek);
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
