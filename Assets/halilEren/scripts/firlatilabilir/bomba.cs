using System.Collections;
using UnityEngine;

public class bomba : MonoBehaviour
{
    public GameObject patlamaAlani, vurulmaSesi, tozPartikül, ozelGuc1, ozelGuc2;
    public bool zamanlamali, anlikPatlamali, gecen, sekti, buzBomba, zehirBomba, patlayanBomba, havaiFisek, patladi;
    public float speed = 20f, rotateSpeed, angle;
    public Rigidbody2D rb;
    canKontrol canKontrol;
    oyuncuHareket oyuncuHareket;

    private void Awake()
    {
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();

        if (havaiFisek)
        {
            oyuncuHareket = FindObjectOfType<oyuncuHareket>();
            ozelGuc1 = GameObject.Find("ozelGuc1");
            ozelGuc2 = GameObject.Find("ozelGuc2");
            if (!oyuncuHareket.sagaBakiyor)
            {
                rotateSpeed = -rotateSpeed;
                gameObject.transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

        if (oyuncuHareket.transform.localScale.x == 1)
        {

            if (!havaiFisek)
            {
                rb.velocity = transform.right * speed;
                rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
            }
        }
        if (oyuncuHareket.transform.localScale.x == -1)
        {

            if (!havaiFisek)
            {
                rb.velocity = transform.right * -speed;
                rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
            }
        }
    }
    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        //    Patla();

        if (havaiFisek)
        {
            if (ozelGuc1.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi == "havai_fisek_aciklama")
            {
                if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("qTusu")) || Input.GetKeyDown(KeyCode.JoystickButton4))
                    Patla();
            }
            else if (ozelGuc2.GetComponent<ozelGucKullanmaScripti>().ozelGucAciklamaKeyi == "havai_fisek_aciklama")
            {
                if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("eTusu")) || Input.GetKeyDown(KeyCode.JoystickButton5))
                    Patla();
            }
        }
    }
    private void FixedUpdate()
    {
        if (havaiFisek)
            transform.Translate(Vector3.right * rotateSpeed * Time.deltaTime);
        else
        {
            if (!sekti)
                transform.Rotate(0, 0, rotateSpeed);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("zemin") || collision.gameObject.CompareTag("cimZemin"))
        {
            if (anlikPatlamali)
                Patla();
            if (zamanlamali)
            {
                StartCoroutine(patlamaZamani());
                patlamaAlani.SetActive(true);
            }
            if (sekti)
            {
                if (!zamanlamali)
                    Patla();
            }
            sekti = true;
        }
        if (collision.gameObject.CompareTag("dusman"))
        {
            if (!gecen)
            {
                Instantiate(vurulmaSesi, transform.position, Quaternion.identity);
                Instantiate(tozPartikül, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.01f);
            }
        }
    }
    void Patla()
    {
        if (!patladi)
        {
            patladi = true;

            if (buzBomba || zehirBomba)
            {
                Instantiate(vurulmaSesi, transform.position, Quaternion.identity);
                Instantiate(tozPartikül, transform.position, Quaternion.identity);
            }
            if (patlayanBomba)
            {
                Vector3 olusmaNokta = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
                Instantiate(vurulmaSesi, olusmaNokta, Quaternion.identity);
                Instantiate(tozPartikül, olusmaNokta, Quaternion.identity);
                Collider2D[] dusmanAlanHasari = Physics2D.OverlapCircleAll(olusmaNokta, 1.5f, LayerMask.GetMask("Dusman"));
                for (int i = 0; i < dusmanAlanHasari.Length; i++)
                {
                    if (dusmanAlanHasari[i].GetComponent<dusmanHasar>() != null)
                        dusmanAlanHasari[i].GetComponent<dusmanHasar>().hasarAl(50, "patlayanBomba");
                }
                Collider2D[] oyuncuAlanHasari = Physics2D.OverlapCircleAll(olusmaNokta, 1.5f, LayerMask.GetMask("Oyuncu"));
                for (int i = 0; i < oyuncuAlanHasari.Length; i++)
                {
                    if (oyuncuAlanHasari[i].name == "Oyuncu")
                    {
                        canKontrol = FindObjectOfType<canKontrol>();
                        canKontrol.canAzalmasi(50, "patlayanBomba");
                        Destroy(gameObject);
                    }
                }
            }
            if (havaiFisek)
            {
                Instantiate(vurulmaSesi, transform.position, Quaternion.identity);
                Instantiate(tozPartikül, transform.position, Quaternion.identity);
                Collider2D[] dusmanAlanHasari = Physics2D.OverlapCircleAll(transform.position, 1.5f, LayerMask.GetMask("Dusman"));
                for (int i = 0; i < dusmanAlanHasari.Length; i++)
                {
                    if (dusmanAlanHasari[i].GetComponent<dusmanHasar>() != null)
                        dusmanAlanHasari[i].GetComponent<dusmanHasar>().hasarAl(50, "havaiFisek");
                }
                Collider2D[] oyuncuAlanHasari = Physics2D.OverlapCircleAll(transform.position, 1.5f, LayerMask.GetMask("Oyuncu"));
                for (int i = 0; i < oyuncuAlanHasari.Length; i++)
                {
                    if (oyuncuAlanHasari[i].name == "Oyuncu")
                    {
                        canKontrol = FindObjectOfType<canKontrol>();
                        canKontrol.canAzalmasi(50, "havaiFisek");
                        Destroy(gameObject);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
    IEnumerator patlamaZamani()
    {
        yield return new WaitForSeconds(2);
        Patla();
    }
}

