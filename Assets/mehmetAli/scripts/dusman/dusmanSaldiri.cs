using System.Collections;
using UnityEngine;

public class dusmanSaldiri : MonoBehaviour
{
    public bool katana, tekagi, yumi, shuriken, tetsubo, arbalet, patlayan;
    dusmanHasar dusmanHasar;
    canKontrol canKontrol;
    public AnimationClip saldiriAnimasyon1, saldiriAnimasyon2;
    public GameObject sagaOk, solaOk;
    public dusman dusman;
    public Transform saldiriPos;
    public float saldirmadanOnceBekleTimer, saldirdiktanSonraTimer, davranmaMesafesi, atilmaGucu, saldiriAlan, hasar, atilmaMiktar;
    public bool oyuncuyaYakin, saldirdiktanSonraBekliyor, saldiriyor, suAndaOkAtiyor;

    void Start()
    {
        dusmanHasar = GetComponent<dusmanHasar>();
    }
    void Update()
    {
        if (saldiriyor)
            dusman.dusmanCimSes.SetActive(false);
    }
    public void saldirKos()
    {
        if (!saldiriyor)
        {
            if (saldirmadanOnceBekleTimer < 1)
            {
                dusman.animator.SetBool("kosma", false);
                dusman.dusmanCimSes.SetActive(false);
                saldirmadanOnceBekleTimer += Time.deltaTime;
            }
            else if (saldirmadanOnceBekleTimer >= 1)
            {
                saldiriyor = true;
                if (katana)
                {
                    if (atilmaMiktar < 2)
                    {
                        atilmaMiktar++;
                        if (Random.Range(0, 3) == 1)
                            atil();
                        else
                            StartCoroutine(saldir());
                    }
                    else
                    {
                        atilmaMiktar = 0f;
                        StartCoroutine(saldir());
                    }
                }
                else if (tekagi || tetsubo)
                    StartCoroutine(saldir());
                else if (yumi || shuriken || arbalet || patlayan)
                    StartCoroutine(okZamanlayici());
            }
        }
    }
    IEnumerator saldir()
    {
        atilmaMiktar = 0f;
        dusman.animator.SetBool("kosma", false);
        dusman.dusmanCimSes.SetActive(false);
        dusman.animator.SetBool("saldiri", true);

        if (tetsubo)
            yield return new WaitForSeconds(0.25f);
        Collider2D[] oyuncuAlanHasari = Physics2D.OverlapCircleAll(transform.position, saldiriAlan, LayerMask.GetMask("Oyuncu"));
        for (int i = 0; i < oyuncuAlanHasari.Length; i++)
        {
            if (oyuncuAlanHasari[i].name == "Oyuncu")
            {
                canKontrol = FindObjectOfType<canKontrol>();
                canKontrol.canAzalmasi(hasar, "kesici");
            }
        }

        if (!saldirdiktanSonraBekliyor)
            StartCoroutine(saldirdiktanSonraBekle());
    }
    void atil()
    {
        dusman.animator.SetBool("kosma", false);
        dusman.dusmanCimSes.SetActive(false);
        dusman.animator.SetTrigger("atilma");

        if (transform.position.x > dusman.oyuncu.transform.position.x)
            dusman.rb.velocity = Vector2.left * atilmaGucu;
        else
            dusman.rb.velocity = Vector2.right * atilmaGucu;

        if (!saldirdiktanSonraBekliyor)
            StartCoroutine(saldirdiktanSonraBekle());
    }
    IEnumerator okZamanlayici()
    {
        if (!suAndaOkAtiyor)
        {
            suAndaOkAtiyor = true;
            dusman.animator.SetBool("kosma", false);
            dusman.animator.SetBool("saldiri", true);
            if (arbalet)
            {
                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForSeconds(0.3f);
                    if (dusman.sagaBakiyor)
                        Instantiate(sagaOk, transform.position, sagaOk.transform.rotation);
                    if (dusman.solaBakiyor)
                        Instantiate(solaOk, transform.position, solaOk.transform.rotation);
                }
            }
            else
            {
                yield return new WaitForSeconds(saldiriAnimasyon1.length);
                if (dusman.sagaBakiyor)
                    Instantiate(sagaOk, transform.position, sagaOk.transform.rotation);
                if (dusman.solaBakiyor)
                    Instantiate(solaOk, transform.position, solaOk.transform.rotation);
            }
            dusman.animator.SetBool("saldiri", false);
            dusman.animator.SetBool("firlatti", true);
            yield return new WaitForSeconds(saldiriAnimasyon2.length);
            dusman.animator.SetBool("firlatti", false);
            suAndaOkAtiyor = false;
            if (!dusmanHasar.donuyor)
                StartCoroutine(saldirdiktanSonraBekle());
        }
    }
    IEnumerator saldirdiktanSonraBekle()
    {
        yield return new WaitForSeconds(saldiriAnimasyon1.length);
        dusman.animator.SetBool("saldiri", false);
        saldirdiktanSonraBekliyor = true;
        yield return new WaitForSeconds(saldirdiktanSonraTimer);
        saldiriyor = false;
        saldirdiktanSonraBekliyor = false;
        saldirmadanOnceBekleTimer = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(saldiriPos.position, saldiriAlan);
    }
}