using System.Collections;
using UnityEngine;

public class dusmanSaldiri : MonoBehaviour
{
    public bool katana, tekagi, yumi, shuriken, tetsubo, arbalet, patlayan, topcu;
    dusmanHasar dusmanHasar;
    canKontrol canKontrol;
    public AnimationClip saldiriAnimasyon1, saldiriAnimasyon2;
    public GameObject sagaOk, solaOk;
    public dusman dusman;
    public Transform saldiriPos;
    public float saldirmadanOnceBekleTimer, saldirmadanOnceBekleme, saldirdiktanSonraTimer, davranmaMesafesi, atilmaGucu, saldiriAlan, hasar, atilmaMiktar;
    public bool oyuncuyaYakin, saldirdiktanSonraBekliyor, atildiktanSonraBekliyor, saldiriyor, suAndaOkAtiyor,hazirlikta;

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
            if (saldirmadanOnceBekleTimer < saldirmadanOnceBekleme)
            {
                dusman.animator.SetBool("kosma", false);
                dusman.dusmanCimSes.SetActive(false);
                saldirmadanOnceBekleTimer += Time.deltaTime;
            }
            else if (saldirmadanOnceBekleTimer >= saldirmadanOnceBekleme)
            {
                saldiriyor = true;
                if (dusman.oyuncuSagda)
                    dusman.sagaBak();
                if (dusman.oyuncuSolda)
                    dusman.solaBak();
                if (katana)
                {
                    if (!atildiktanSonraBekliyor)
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
                }
                else if (tekagi || tetsubo)
                    StartCoroutine(saldir());
                else if (yumi || shuriken || arbalet || patlayan || topcu)
                    StartCoroutine(okZamanlayici());
            }
        }
    }
    IEnumerator saldir()
    {
        yield return new WaitForSeconds(0);
        atilmaMiktar = 0f;
        dusman.animator.SetBool("kosma", false);
        dusman.dusmanCimSes.SetActive(false);
        dusman.animator.SetBool("saldiriHazirlik", true);
        hazirlikta = true;
        StartCoroutine(saldirHazirlik());

    }
    void atil()
    {
        dusman.animator.SetBool("kosma", false);
        dusman.dusmanCimSes.SetActive(false);
        dusman.animator.SetBool("atil", true);

        if (transform.position.x > dusman.oyuncu.transform.position.x)
            dusman.rb.velocity = Vector2.left * atilmaGucu;
        else
            dusman.rb.velocity = Vector2.right * atilmaGucu;

        if (!atildiktanSonraBekliyor)
            StartCoroutine(atildiktanSonraBekle());
    }
    IEnumerator okZamanlayici()
    {
        if (!suAndaOkAtiyor)
        {
            suAndaOkAtiyor = true;
            if (!topcu)
            {
                dusman.animator.SetBool("kosma", false);
                dusman.animator.SetBool("saldiri", true);
            }
            if (arbalet)
            {
                for (int i = 0; i < 3; i++)
                {
                    yield return new WaitForSeconds(0.3f);
                    if (dusman.sagaBakiyor)
                    {
                        sagaOk.GetComponent<projectile>().saga = true;
                        Instantiate(sagaOk, transform.position, sagaOk.transform.rotation);
                    }
                    if (dusman.solaBakiyor)
                    {
                        sagaOk.GetComponent<projectile>().saga = false;
                        Instantiate(solaOk, transform.position, solaOk.transform.rotation);
                    }
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
            if (!topcu)
            {
                dusman.animator.SetBool("saldiri", false);
                dusman.animator.SetBool("firlatti", true);
            }
            yield return new WaitForSeconds(saldiriAnimasyon2.length);
            if (!topcu)
                dusman.animator.SetBool("firlatti", false);
            suAndaOkAtiyor = false;
            if (!dusmanHasar.donuyor)
                StartCoroutine(saldirdiktanSonraBekle());
        }
    }
    IEnumerator saldirdiktanSonraBekle()
    {
        yield return new WaitForSeconds(saldiriAnimasyon1.length);

        hazirlikta = false;
        dusman.animator.SetBool("saldiri", false);
        saldirdiktanSonraBekliyor = true;
        if (dusman.kaciyor)
            yield return new WaitForSeconds(saldirdiktanSonraTimer * 4);
        else
            yield return new WaitForSeconds(saldirdiktanSonraTimer);
        saldiriyor = false;
        saldirdiktanSonraBekliyor = false;
        saldirmadanOnceBekleTimer = 0f;
        if (dusman.oyuncuSagda)
            dusman.sagaBak();
        if (dusman.oyuncuSolda)
            dusman.solaBak();
    }
    IEnumerator atildiktanSonraBekle()
    {
        yield return new WaitForSeconds(saldiriAnimasyon2.length);
        dusman.animator.SetBool("atil", false);
        atildiktanSonraBekliyor = true;
        yield return new WaitForSeconds(saldirdiktanSonraTimer);
        saldiriyor = false;
        atildiktanSonraBekliyor = false;
        saldirmadanOnceBekleTimer = 0f;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(saldiriPos.position, saldiriAlan);
    }
    IEnumerator saldirHazirlik()
    {
        yield return new WaitForSeconds(0.5f);

        dusman.animator.SetBool("saldiriHazirlik", false);

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
}