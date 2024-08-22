using UnityEngine;

public class rastgeleYadigarDusurmeScripti : MonoBehaviour
{
    public GameObject dusecekOlanYadigar;
    public int rastgeleSayi, dusmeIhtimali;
    public bool yadigarDustu;
    antikaYadigarKontrol antikaYadigarKontrol;

    void Start()
    {

    }
    public void yadigarDusurme()
    {
        if (antikaYadigarKontrol.yadigarDusebilir)
        {
            dusmeIhtimali = 95;
            if (!yadigarDustu)
            {
                yadigarDustu = true;
                rastgeleSayi = Random.Range(0, 100);
                if (dusmeIhtimali > rastgeleSayi)
                {
                    Instantiate(dusecekOlanYadigar, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                }
            }
        }
    }
}
