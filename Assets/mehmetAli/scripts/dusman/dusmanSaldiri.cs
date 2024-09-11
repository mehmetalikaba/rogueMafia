using System.Collections;
using System.Threading;
using UnityEngine;

public class dusmanSaldiri : MonoBehaviour
{
    public bool katana, tekagi, yumi, shuriken, tetsubo, arbalet, patlayan, topcu;

    public AnimationClip saldiriAnimasyon1, saldiriAnimasyon2;
    public GameObject sagaOk, solaOk, zehirSagaOk, zehirSolaOk;
    public dusman dusman;
    public Transform saldiriPos;
    public float saldirmadanOnceBekleTimer, saldirmadanOnceBekleme, saldirdiktanSonraTimer, davranmaMesafesi, atilmaGucu, saldiriAlan, hasar, atilmaMiktar;
    public bool oyuncuyaYakin, saldirdiktanSonraBekliyor, atildiktanSonraBekliyor, saldiriyor, suAndaOkAtiyor;
    antikaYadigarKontrol antikaYadigarKontrol;
    dusmanHasar dusmanHasar;
    canKontrol canKontrol;
    float timer = 5f;

    //Hazırlik
    public bool hazirlikta, hazirlaniyor;

    void Start()
    {
        dusmanHasar = GetComponent<dusmanHasar>();
        antikaYadigarKontrol = FindObjectOfType<antikaYadigarKontrol>();
    }
    private void Update()
    {
        while (saldiriyor)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 5f;
                saldiriyor = false;
            }
        }
    }
    public void saldirKos()
    {
        if (!saldiriyor)
        {
            if (saldirmadanOnceBekleTimer < saldirmadanOnceBekleme)
            {
                dusman.animator.SetBool("kosma", false);
                saldirmadanOnceBekleTimer += Time.deltaTime;
            }
            else if (saldirmadanOnceBekleTimer >= saldirmadanOnceBekleme && (!dusman.kaciyor || !dusman.yuruyor))
            {
                saldiriyor = true;
                dusman.oyuncuyaBak();
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
                else if (yumi || shuriken || arbalet || patlayan || topcu)
                    StartCoroutine(okZamanlayici());
            }
        }
    }
    IEnumerator saldir()
    {
        yield return new WaitForSeconds(0);
        hazirlikta = true;
        atilmaMiktar = 0f;
        dusman.animator.SetBool("kosma", false);
        dusman.animator.SetBool("saldiriHazirlik", true);
        if (!hazirlaniyor)
            StartCoroutine(hazirlik());
    }
    void atil()
    {
        dusman.animator.SetBool("kosma", false);
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
        if (!suAndaOkAtiyor && dusman.oyuncuGorusAcisinda)
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
                        if (antikaYadigarKontrol.hangiYadigarAktif[2])
                            Instantiate(zehirSagaOk, transform.position, zehirSagaOk.transform.rotation);
                        else if (!antikaYadigarKontrol.hangiYadigarAktif[2])
                            Instantiate(sagaOk, transform.position, sagaOk.transform.rotation);
                    }
                    if (dusman.solaBakiyor)
                    {
                        sagaOk.GetComponent<projectile>().saga = false;
                        if (antikaYadigarKontrol.hangiYadigarAktif[2])
                            Instantiate(zehirSolaOk, transform.position, zehirSolaOk.transform.rotation);
                        else if (!antikaYadigarKontrol.hangiYadigarAktif[2])
                            Instantiate(solaOk, transform.position, solaOk.transform.rotation);
                    }
                }
            }
            else
            {
                yield return new WaitForSeconds(saldiriAnimasyon1.length);
                if (dusman.sagaBakiyor)
                {
                    if (antikaYadigarKontrol.hangiYadigarAktif[2] && !patlayan)
                        Instantiate(zehirSagaOk, transform.position, zehirSagaOk.transform.rotation);
                    else if (!antikaYadigarKontrol.hangiYadigarAktif[2] || patlayan)
                        Instantiate(sagaOk, transform.position, sagaOk.transform.rotation);
                }
                if (dusman.solaBakiyor)
                {
                    if (antikaYadigarKontrol.hangiYadigarAktif[2] && !patlayan)
                        Instantiate(zehirSolaOk, transform.position, zehirSolaOk.transform.rotation);
                    else if (!antikaYadigarKontrol.hangiYadigarAktif[2] || patlayan)
                        Instantiate(solaOk, transform.position, solaOk.transform.rotation);
                }
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
        dusman.oyuncuyaBak();
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
    IEnumerator hazirlik()
    {
        hazirlaniyor = true;
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
                if (tekagi)
                    canKontrol.etmenler[2] = true;
                if (tetsubo)
                    canKontrol.etmenler[4] = true;
            }
        }
        if (!saldirdiktanSonraBekliyor)
            StartCoroutine(saldirdiktanSonraBekle());



    }
}