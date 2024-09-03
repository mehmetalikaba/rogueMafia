using System.Collections;
using UnityEngine;

public class dusmanZeminKontrol : MonoBehaviour
{
    public bool zeminde, cikti, yurut;
    public dusman dusman;
    public BoxCollider2D zeminCollider;

    void Awake()
    {
        zeminCollider = GetComponent<BoxCollider2D>();
        zeminCollider.enabled = false;
        StartCoroutine(beklemeSuresi());
    }
    void FixedUpdate()
    {
        if (!zeminde)
        {
            Debug.Log("zeminden cikti");
            if (dusman.devriyeModunda)
            {
                Debug.Log("durduruldu");
                dusman.devriyeModunda = false;
                if (dusman.solBekle || dusman.solaYuru)
                    StartCoroutine(sagaGotur());
                else if (dusman.sagaYuru || dusman.sagBekle)
                    StartCoroutine(solaGotur());
            }
            else if (dusman.saldiriModunda)
            {
                yurut = true;
                dusman.kontrollerAcik = false;
                dusman.saldiriModunda = false;
                dusman.kaciyor = false;
                if (dusman.sagaBakiyor)
                    dusman.solaBak();
                else if (dusman.solaBakiyor)
                    dusman.sagaBak();
            }
        }
        if (yurut)
        {
            if (dusman.menzilli)
            {
                if (dusman.oyuncuSolda && dusman.solaBakiyor)
                {
                    dusman.yuru();
                    dusman.kontrolTimer = 0f;
                }
                else if (dusman.oyuncuSagda && dusman.sagaBakiyor)
                {
                    dusman.yuru();
                    dusman.kontrolTimer = 0f;
                }
                else
                {
                    yurut = false;
                    dusman.kontrolTimer = 0f;
                    dusman.kontrollerAcik = true;
                }
            }
            if (dusman.yakin)
            {
                if ((dusman.oyuncuSolda && dusman.solaBakiyor) && (dusman.oyuncuyaYakinlik < dusman.dusmanSaldiri.davranmaMesafesi / 4))
                {
                    dusman.yuru();
                    dusman.kontrolTimer = 0f;
                }
                else if ((dusman.oyuncuSagda && dusman.sagaBakiyor) && (dusman.oyuncuyaYakinlik < dusman.dusmanSaldiri.davranmaMesafesi / 4))
                {
                    dusman.yuru();
                    dusman.kontrolTimer = 0f;
                }
                else
                {
                    yurut = false;
                    dusman.kontrolTimer = 0f;
                    dusman.kontrollerAcik = true;
                }
            }
        }
    }
    IEnumerator sagaGotur()
    {
        Debug.Log("SOLDA <==> corotin girdi");
        yield return new WaitForSeconds(0.5f);
        dusman.sagaBak();
        yield return new WaitForSeconds(0.5f);
        dusman.devriyeModunda = true;
        dusman.sagaYuru = true;
        yield return new WaitForSeconds(0.25f);
        Debug.Log("SOLDA <==> corotin cikti");
    }
    IEnumerator solaGotur()
    {
        Debug.Log("SAGDA <==> corotin girdi");
        yield return new WaitForSeconds(0.5f);
        dusman.solaBak();
        yield return new WaitForSeconds(0.5f);
        dusman.devriyeModunda = true;
        dusman.solaYuru = true;
        yield return new WaitForSeconds(0.25f);
        Debug.Log("SAGDA <==> corotin cikti");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("zemin") || collision.gameObject.CompareTag("cimZemin"))
        {
            zeminde = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("zemin") || collision.gameObject.CompareTag("cimZemin"))
        {
            zeminde = false;
        }
    }
    IEnumerator beklemeSuresi()
    {
        yield return new WaitForSeconds(0.25f);
        if (!zeminCollider.enabled)
            zeminCollider.enabled = true;
    }
}
