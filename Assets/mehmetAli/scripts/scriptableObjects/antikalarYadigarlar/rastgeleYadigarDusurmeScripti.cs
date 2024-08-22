using UnityEngine;

public class rastgeleYadigarDusurmeScripti : MonoBehaviour
{
    public GameObject dusecekOlanYadigar;
    public int rastgeleSayi, dusmeIhtimali;
    public bool yadigarDustu;

    void Start()
    {

    }
    public void yadigarDusurme()
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
