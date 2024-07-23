using System.Collections;
using UnityEngine;

public class oyuncuEfektYoneticisi : MonoBehaviour
{
    public bool cimde, tasda;

    public GameObject tasYurumeSes;
    public AudioSource tasZiplamaSes, tasDusmeSes;

    public GameObject cimYurumeSes;
    public AudioSource cimZiplamaSes, cimDusmeSes;

    public Transform partikülKonum;
    public GameObject tozPartikül, dusmeTozPartikül, atilmaPartikül, varmaPartikül;
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
            Instantiate(tozPartikül, partikülKonum.position, Quaternion.identity);
            timer = 0;
        }
    }
    public void ZiplamaToz()
    {

        Instantiate(tozPartikül, partikülKonum.position, Quaternion.identity);
    }
    public void DusmeToz()
    {
        Instantiate(dusmeTozPartikül, partikülKonum.position, Quaternion.identity);

    }

    public void AtilmaEfekt()
    {
        Instantiate(atilmaPartikül, partikülKonum.position, Quaternion.identity);
        StartCoroutine(beklemeSuresi());

    }
    IEnumerator beklemeSuresi()
    {
        yield return new WaitForSeconds(0.05f);
        Instantiate(varmaPartikül, partikülKonum.position, Quaternion.identity);
    }
}
