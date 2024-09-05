using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kesilebilirBitki : MonoBehaviour
{
    public AudioSource sfx;
    public GameObject fx;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public bool oyuncuYakin;

    SpriteRenderer spriteRenderer;
    public GameObject kesilmisCizim;
    public void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 2f, LayerMask.GetMask("Oyuncu"));

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("solTikTusu")) && oyuncuYakin && !oyuncuSaldiriTest.yumruk1)
            kesildi();
    }
    public void kesildi()
    {
        sfx.Play();
        Instantiate(fx,transform.position,transform.rotation);
        spriteRenderer.enabled = false;
        kesilmisCizim.SetActive(true);
    }
}
