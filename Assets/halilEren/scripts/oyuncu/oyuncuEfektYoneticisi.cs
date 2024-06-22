using System.Collections;
using UnityEngine;

public class oyuncuEfektYoneticisi : MonoBehaviour
{
    public GameObject yurumeSes;
    public AudioSource ziplamaSes, dusmeSes;

    public Transform partik�lKonum;
    public GameObject tozPartik�l, dusmeTozPartik�l,atilmaPartik�l,varmaPartik�l;
    public float time;
    float timer;

    Rigidbody2D rb;
    Vector2 movementX;
    public bool zeminde;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movementX.x = rb.velocity.x;
        if (movementX.x == 0||!zeminde)
        {
            yurumeSes.SetActive(false);
        }
        else if(movementX.x != 0&&zeminde)
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
