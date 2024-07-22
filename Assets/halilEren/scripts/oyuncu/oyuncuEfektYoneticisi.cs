using System.Collections;
using UnityEngine;

public class oyuncuEfektYoneticisi : MonoBehaviour
{
    public GameObject yurumeSes;
    public AudioSource ziplamaSes, dusmeSes;

    public Transform partik�lKonum;
    public GameObject tozPartik�l, dusmeTozPartik�l, atilmaPartik�l, varmaPartik�l;
    public float time;
    float timer;

    Rigidbody2D rb;

    oyuncuHareket oyuncuHareket;
    oyuncuSaldiriTest oyuncuSaldiriTest;

    void Start()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((oyuncuHareket.movementX.x == 0 || oyuncuHareket.havada) || oyuncuSaldiriTest.solTikTiklandi || oyuncuSaldiriTest.sagTikTiklandi || oyuncuHareket.hareketKilitli)
        {
            yurumeSes.SetActive(false);
        }
        else if (oyuncuHareket.movementX.x != 0 && !oyuncuHareket.havada)
        {
            yurumeSes.SetActive(true);
            YurumeToz();
        }
    }
    public void ZiplamaSesi()
    {
        ziplamaSes.Play();
    }
    public void DusmeSesi()
    {
        dusmeSes.Play();
    }


    void YurumeToz()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            Instantiate(tozPartik�l, partik�lKonum.position, Quaternion.identity);
            timer = 0;
        }
    }
    public void ZiplamaToz()
    {

        Instantiate(tozPartik�l, partik�lKonum.position, Quaternion.identity);
    }
    public void DusmeToz()
    {
        Instantiate(dusmeTozPartik�l, partik�lKonum.position, Quaternion.identity);

    }

    public void AtilmaEfekt()
    {
        Instantiate(atilmaPartik�l, partik�lKonum.position, Quaternion.identity);
        StartCoroutine(beklemeSuresi());

    }
    IEnumerator beklemeSuresi()
    {
        yield return new WaitForSeconds(0.05f);
        Instantiate(varmaPartik�l, partik�lKonum.position, Quaternion.identity);
    }
}
