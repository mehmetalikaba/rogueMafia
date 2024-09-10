using System.Collections;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public int kacDusman;
    public bool dusmandan, saga, bomba, dondurma, zehirleme, carpti, yonel, arbalet, oyuncuKonumAldi;
    public float speed, rotateSpeed, angle;
    public GameObject vurulmaSesi, tozPartikul, oyuncu;
    Rigidbody2D rb;
    oyuncuSaldiriTest oyuncuSaldiriTest;
    oyuncuHareket oyuncuHareket;
    canKontrol canKontrol;
    BoxCollider2D boxCollider2d;
    Vector2 oyuncuKonum;

    private void Awake()
    {
        oyuncu = GameObject.Find("Oyuncu");
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        if (!oyuncuKonumAldi)
        {
            oyuncuKonum = new Vector2(oyuncu.transform.position.x, oyuncu.transform.position.y);
            oyuncuKonumAldi = true;
        }

        if (arbalet && !dusmandan)
        {
            if (dusmandan)
            {

            }
            else
            {
                oyuncuHareket = FindObjectOfType<oyuncuHareket>();
                if (oyuncuHareket.sagaBakiyor)
                    saga = true;
            }
        }
        if (!arbalet)
            rb.velocity = transform.right * speed;
        if (!dusmandan)
            speed = oyuncuSaldiriTest.sonSaldiriMenzili;
        if (bomba)
            StartCoroutine(bombeli());
    }
    IEnumerator bombeli()
    {
        boxCollider2d.enabled = false;
        yield return new WaitForSeconds(0.25f);
        yonel = true;
        yield return new WaitForSeconds(0.15f);
        boxCollider2d.enabled = true;
    }
    void FixedUpdate()
    {
        if (arbalet && !carpti)
            if (saga)
                transform.Translate(Vector3.right * 8.5f * Time.deltaTime);
            else
                transform.Translate(Vector3.left * 8.5f * Time.deltaTime);

        if (!carpti)
        {
            Vector2 v = GetComponent<Rigidbody2D>().velocity;
            angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.Rotate(0, 0, rotateSpeed);
            if (yonel)
                transform.position = Vector3.MoveTowards(transform.position, oyuncuKonum, speed / 4 * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("zemin") || collision.gameObject.CompareTag("cimZemin"))
        {
            if (bomba)
            {
                Collider2D[] oyuncuAlanHasari = Physics2D.OverlapCircleAll(transform.position, 1.5f, LayerMask.GetMask("Oyuncu"));
                for (int i = 0; i < oyuncuAlanHasari.Length; i++)
                {
                    if (oyuncuAlanHasari[i].name == "Oyuncu")
                    {
                        canKontrol = FindObjectOfType<canKontrol>();
                        canKontrol.canAzalmasi(25, "patlayanDusman");
                        if (dondurma)
                            canKontrol.etmenler[0] = true;
                        if (zehirleme)
                            canKontrol.etmenler[1] = true;
                        Destroy(gameObject);
                    }
                }
            }
            carpti = true;
            boxCollider2d.enabled = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.freezeRotation = true;
            Instantiate(vurulmaSesi, transform.position, Quaternion.identity);
            Instantiate(tozPartikul, transform.position, Quaternion.identity);
            speed = 0;
            gameObject.tag = "Untagged";
        }
    }
}

