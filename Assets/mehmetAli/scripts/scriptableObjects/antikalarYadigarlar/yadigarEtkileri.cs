using System.Collections;
using UnityEngine;

public class yadigarEtkileri : MonoBehaviour
{
    public bool barutFicisi, patlayanOk;
    public float barutFicisiTimer;

    canKontrol canKontrol;

    void Start()
    {

    }

    void Update()
    {
        if (barutFicisi)
            barutFicisiPatla();
        if (patlayanOk)
            StartCoroutine(okPatlama());

    }
    public void barutFicisiPatla()
    {
        barutFicisiTimer += Time.deltaTime;
        if (barutFicisiTimer > 2)
        {
            barutFicisiTimer = 0f;
            Debug.Log(gameObject.name + " patladi");
            Collider2D[] alanHasari = Physics2D.OverlapCircleAll(transform.position, 5, LayerMask.GetMask("Oyuncu"));
            for (int i = 0; i < alanHasari.Length; i++)
            {
                if (alanHasari[i].name == "Oyuncu")
                {
                    canKontrol = FindObjectOfType<canKontrol>();
                    canKontrol.canAzalmasi(5, "barutFicisi");
                    Destroy(gameObject);
                }
            }
        }
    }
    IEnumerator okPatlama()
    {
        patlayanOk = false;
        yield return new WaitForSeconds(0.2f);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
        Debug.Log(gameObject.name + " patladi");
        yield return new WaitForSeconds(0.1f);
        Collider2D[] alanHasari = Physics2D.OverlapCircleAll(transform.position, 2, LayerMask.GetMask("Dusman"));
        for (int i = 0; i < alanHasari.Length; i++)
        {
            if (alanHasari[i].name != "zeminkontrol")
                alanHasari[i].GetComponent<dusmanHasar>().hasarAl(5, "okPatlamasi");
        }
    }
}
