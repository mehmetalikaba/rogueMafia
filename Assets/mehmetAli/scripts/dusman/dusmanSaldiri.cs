﻿using System.Collections;
using UnityEngine;

public class dusmanSaldiri : MonoBehaviour
{
    public bool katana, tekagi, yumi, shuriken, tetsubo, arbalet;
    dusmanHasar dusmanHasar;
    canKontrol canKontrol;
    public GameObject sagaOk, solaOk;
    public dusman dusman;
    public Transform saldiriPos;
    public float saldirmadanOnceBekleTimer, saldirdiktanSonraTimer, davranmaMesafesi, atilmaGucu, saldiriAlan, hasar, atilmaMiktar;
    public bool oyuncuyaYakin, saldirdiktanSonraBekliyor, saldirabilir, saldiriyor;
    public bool atiyor, okFirlat, okAtiyor, suAndaOkAtiyor;
    public float okTimer;

    void Start()
    {
        dusmanHasar = FindObjectOfType<dusmanHasar>();
    }
    void Update()
    {
        if (suAndaOkAtiyor)
        {
            dusman.animator.SetBool("yurume", false);
        }
    }
    public void saldirKos()
    {
        if (!saldiriyor)
        {
            if (saldirmadanOnceBekleTimer < 1)
            {
                dusman.animator.SetBool("yurume", false);
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
                            saldir();
                    }
                    else
                    {
                        atilmaMiktar = 0f;
                        saldir();
                    }
                }
                else if (tekagi || tetsubo)
                    saldir();
                else if (yumi)
                    StartCoroutine(okZamanlayici());
            }
        }
    }
    void saldir()
    {
        atilmaMiktar = 0f;
        dusman.animator.SetBool("yurume", false);
        dusman.dusmanCimSes.SetActive(false);
        dusman.animator.SetTrigger("saldiri");

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
        dusman.animator.SetBool("yurume", false);
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
        if (!atiyor)
        {
            suAndaOkAtiyor = true;
            dusman.animator.SetBool("yurume", false);
            yield return new WaitForSeconds(0.7f);
            dusman.animator.SetTrigger("ok");
            okTimer = 0f;
        }
        atiyor = true;
        yield return new WaitForSeconds(0.7f);
        if (!dusmanHasar.donuyor)
        {
            if (dusman.oyuncuSolda)
                Instantiate(solaOk, transform.position, solaOk.transform.rotation);
            if (dusman.oyuncuSagda)
                Instantiate(sagaOk, transform.position, sagaOk.transform.rotation);
            suAndaOkAtiyor = false;
            StartCoroutine(saldirdiktanSonraBekle());
        }
    }
    IEnumerator saldirdiktanSonraBekle()
    {
        saldirdiktanSonraBekliyor = true;
        yield return new WaitForSeconds(saldirdiktanSonraTimer);
        saldirabilir = false;
        saldiriyor = false;
        atiyor = false;
        saldirdiktanSonraBekliyor = false;
        saldirmadanOnceBekleTimer = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(saldiriPos.position, saldiriAlan);
    }
}