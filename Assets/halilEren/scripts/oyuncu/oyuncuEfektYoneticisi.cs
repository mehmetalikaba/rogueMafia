using System.Collections;
using UnityEngine;

public class oyuncuEfektYoneticisi : MonoBehaviour
{
    public bool cimde, tasda;

    public GameObject tasYurumeSes;
    public AudioSource tasZiplamaSes, tasDusmeSes;

    public GameObject cimYurumeSes;
    public AudioSource cimZiplamaSes, cimDusmeSes;

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
            if(tasda)
            {
                tasYurumeSes.SetActive(false);
            }
            if (cimde)
            {
                cimYurumeSes.SetActive(false);
            }
        }
        else if (oyuncuHareket.movementX.x != 0 && !oyuncuHareket.havada)
        {
            if (tasda)
            {
                tasYurumeSes.SetActive(true);
            }
            if (cimde)
            {
                cimYurumeSes.SetActive(true);
            }
            YurumeToz();
        }
    }
    public void ZiplamaSesi()
    {
        if(tasda)
        {
            tasZiplamaSes.Play();

        }
        if (cimde)
        {
            cimZiplamaSes.Play();

        }
    }
    public void DusmeSesi()
    {
        if (tasda)
        {
            tasDusmeSes.Play();

        }
        if (cimde)
        {
            cimDusmeSes.Play();

        }
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
