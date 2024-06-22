using System.Collections;
using UnityEngine;

public class oyuncuEfektYoneticisi : MonoBehaviour
{
    public GameObject yurumeSes;
    public AudioSource ziplamaSes, dusmeSes;

    public Transform partikülKonum;
    public GameObject tozPartikül, dusmeTozPartikül,atilmaPartikül,varmaPartikül;
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
