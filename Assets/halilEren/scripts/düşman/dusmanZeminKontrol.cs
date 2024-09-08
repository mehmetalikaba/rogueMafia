using System.Collections;
using UnityEngine;

public class dusmanZeminKontrol : MonoBehaviour
{
    public bool zeminde, cikti, saldirt;
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
        if (dusman == null)
            Destroy(this);

        if (!zeminde)
        {
            if (dusman.devriyeModunda)
            {
                dusman.devriyeModunda = false;
                if (dusman.solBekle || dusman.solaYuru)
                    StartCoroutine(sagaGotur());
                else if (dusman.sagaYuru || dusman.sagBekle)
                    StartCoroutine(solaGotur());
            }
            else if (dusman.saldiriModunda)
            {
                saldirt = true;
                dusman.kontrollerAcik = false;
                dusman.saldiriModunda = false;
                dusman.rb.constraints = RigidbodyConstraints2D.FreezeAll;
                dusman.kaciyor = false;
                if (dusman.sagaBakiyor)
                    dusman.solaBak();
                else if (dusman.solaBakiyor)
                    dusman.sagaBak();
            }
        }
        
        if (saldirt)
        {
            if (dusman.menzilli)
            {
                dusman.dusmanSaldiri.saldirKos();
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
                    saldirt = false;
                    dusman.kontrolTimer = 0f;
                    dusman.kontrollerAcik = true;
                }
            }
        }
    }
    IEnumerator sagaGotur()
    {
        yield return new WaitForSeconds(0.5f);
        dusman.sagaBak();
        yield return new WaitForSeconds(0.5f);
        dusman.devriyeModunda = true;
        dusman.sagaYuru = true;
        yield return new WaitForSeconds(0.25f);
    }
    IEnumerator solaGotur()
    {
        yield return new WaitForSeconds(0.5f);
        dusman.solaBak();
        yield return new WaitForSeconds(0.5f);
        dusman.devriyeModunda = true;
        dusman.solaYuru = true;
        yield return new WaitForSeconds(0.25f);
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
