using UnityEngine;
using UnityEngine.UI;

public class aniAgaciEfektleri : MonoBehaviour
{
    Animator animator;
    Button buton;
    bool mouseUzerinde, mouseYok;

    public bool kilitAcik, aniPuaniYeterli, yetenekMax;

    public yetenekKontrol yetenekKontrol;
    public envanterKontrol envanterKontrol;
    public yetenekObjesi yetenekObjesi;

    public int kacinciYetenek;
    public string hangiYetenek;

    void Start()
    {
        animator = GetComponent<Animator>();
        envanterKontrol = FindAnyObjectByType<envanterKontrol>();
        yetenekKontrol = FindObjectOfType<yetenekKontrol>();

        if (yetenekObjesi.yetenekSeviyesi == yetenekObjesi.maxSeviye)
        {
            yetenekMax = true;
        }
    }

    void FixedUpdate()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(Input.mousePosition);

        if (rectTransform.rect.Contains(localMousePosition))
        {
            mouseUzerinde = true;
            mouseYok = false;
        }
        else
        {
            mouseUzerinde = false;
            mouseYok = true;
        }


        if (yetenekMax)
        {
            animator.SetBool("gelistirildi", true);
            animator.SetBool("sonme", false);
            animator.SetBool("mouseUzerinde", false);
            animator.SetBool("yanipSonme", false);

            mouseUzerinde = false;
            mouseYok = false;
            kilitAcik = false;
            aniPuaniYeterli = false;

            buton = GetComponent<Button>();
            buton.interactable = false;
        }
        else
        {
            if (yetenekObjesi != null)
            {
                if (yetenekObjesi.yetenekSeviyesi != yetenekObjesi.maxSeviye)
                {
                    if (!kilitAcik)
                    {
                        foreach (var oncekiYetenekler in yetenekObjesi.gerekliYetenekler)
                        {
                            if (oncekiYetenekler.yetenekSeviyesi > 0)
                                kilitAcik = true;
                        }
                    }
                }
                else
                    yetenekMax = true;
            }

            if (mouseUzerinde)
            {
                animator.SetBool("gelistirildi", false);
                animator.SetBool("sonme", false);
                animator.SetBool("mouseUzerinde", true);
                animator.SetBool("yanipSonme", false);
            }
            else if (mouseYok)
            {
                if (kilitAcik)
                {
                    if (envanterKontrol.aniPuani >= yetenekObjesi.gerekliAniPuani)
                        aniPuaniYeterli = true;
                    else if (envanterKontrol.aniPuani <= yetenekObjesi.gerekliAniPuani)
                        aniPuaniYeterli = false;

                    if (aniPuaniYeterli)
                    {
                        animator.SetBool("gelistirildi", false);
                        animator.SetBool("sonme", false);
                        animator.SetBool("mouseUzerinde", false);
                        animator.SetBool("yanipSonme", true);
                    }
                }
                else if (!aniPuaniYeterli || !kilitAcik)
                {
                    animator.SetBool("gelistirildi", false);
                    animator.SetBool("sonme", true);
                    animator.SetBool("mouseUzerinde", false);
                    animator.SetBool("yanipSonme", false);
                }
            }
        }
    }

    public void yetenegiGelistir()
    {
        if (!yetenekMax)
        {
            if (aniPuaniYeterli && kilitAcik)
            {
                Debug.Log(gameObject.name + " <==> ani acildi");
                envanterKontrol.aniPuani -= yetenekObjesi.gerekliAniPuani;
                yetenekKontrol.yetenekButonunaBasti(hangiYetenek, kacinciYetenek);
            }
        }
    }
}
