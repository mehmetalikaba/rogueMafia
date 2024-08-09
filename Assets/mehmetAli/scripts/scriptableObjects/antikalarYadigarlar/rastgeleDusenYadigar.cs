using System.Collections;
using UnityEngine;

public class rastgeleDusenYadigar : MonoBehaviour
{
    public bool oyuncuYakin;
    public float yokOlmaSuresi;
    public GameObject isik;
    public antikaYadigarOzellikleri buYadigarObjesi;
    public antikaYadigarKontrol antikaYadigarKontrol;
    public oyuncuHareket oyuncuHareket;
    public silahKontrol silahKontrol;

    void Start()
    {
        yokOlmaSuresi = 15f;

        antikaYadigarKontrol = FindObjectOfType<antikaYadigarKontrol>();
        silahKontrol = FindObjectOfType<silahKontrol>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));

        if (oyuncuYakin) isik.SetActive(true);
        else isik.SetActive(false);

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin && !oyuncuHareket.atiliyor && !silahKontrol.yerdenAliyor)
            yerdenYadigarAl();

        yokOlmaSuresi -= Time.deltaTime;
        if (yokOlmaSuresi < 0)
            Destroy(gameObject);
    }

    public void yerdenYadigarAl()
    {
        silahKontrol.yerdenAliyor = true;

        for (int i = 0; i < antikaYadigarKontrol.yadigarSlotBos.Length; i++)
        {
            if (antikaYadigarKontrol.yadigarSlotBos[i])
            {
                antikaYadigarKontrol.yadigarSlotBos[i] = false;
                antikaYadigarKontrol.yadigarObjesi[i] = buYadigarObjesi;
                antikaYadigarKontrol.yadigarlarImage[i].sprite = buYadigarObjesi.yadigarIcon;
            }
        }
    }
}
