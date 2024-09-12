using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using XInputDotNetPure;

public class yadigarEtkileri : MonoBehaviour
{
    Gamepad gamepad;
    public GameObject fx;
    public bool barutFicisi, dondurmaFicisi, zehirlemeFicisi, patlayanOk;
    public float barutFicisiTimer;

    canKontrol canKontrol;

    void Start()
    {
        //gamepad = GetComponent<Gamepad>();
    }

    void Update()
    {
        if (barutFicisi || zehirlemeFicisi || dondurmaFicisi)
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
            Collider2D[] alanHasari = Physics2D.OverlapCircleAll(transform.position, 3, LayerMask.GetMask("Oyuncu"));
            for (int i = 0; i < alanHasari.Length; i++)
            {
                if (alanHasari[i].name == "Oyuncu")
                {
                    //gamepad.SetMotorSpeeds(1, 1);
                    StartCoroutine(stopVib());
                    canKontrol = FindObjectOfType<canKontrol>();
                    if (barutFicisi)
                        canKontrol.canAzalmasi(5, "barutFicisi");
                    else if (dondurmaFicisi)
                        canKontrol.etmenler[0] = true;
                    else if (zehirlemeFicisi)
                        canKontrol.etmenler[1] = true;
                    if (fx != null)
                        Instantiate(fx, transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);
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
    IEnumerator stopVib()
    {
        yield return new WaitForSeconds(0.5f);
        gamepad.SetMotorSpeeds(0, 0);
    }
}
