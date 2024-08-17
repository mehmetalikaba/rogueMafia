using UnityEngine;

public class rastgeleSilahDusurmeScripti : MonoBehaviour
{
    public silahOzellikleri dusmaninElindekiSilah;
    public rastgeleDusenSilah rastgeleDusenSilah;
    public GameObject dusecekOlanSilah;
    public SpriteRenderer dusecekOlanSilahinSpriteRenderer;
    public float randomSayi;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public bool silahDusmeli, silahDustu;

    void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
    }

    private void Update()
    {
        if (oyuncuSaldiriTest.yumruk1 || oyuncuSaldiriTest.yumruk2)
            silahDusmeli = true;
    }

    public void silahiDusur(float silahDusurmeIhtimali, float minSilahDusurmeIhtimali, float maxSilahDusurmeIhtimali)
    {
        if (silahDusmeli)
            silahDusurme();
        else
        {
            randomSayi = Random.Range(minSilahDusurmeIhtimali, maxSilahDusurmeIhtimali);

            if (randomSayi < silahDusurmeIhtimali)
                silahDusurme();
        }
    }

    public void silahDusurme()
    {
        if (!silahDustu)
        {
            silahDustu = true;
            rastgeleDusenSilah = dusecekOlanSilah.GetComponent<rastgeleDusenSilah>();
            dusecekOlanSilahinSpriteRenderer = rastgeleDusenSilah.GetComponent<SpriteRenderer>();
            rastgeleDusenSilah.dusenSilah = dusmaninElindekiSilah;
            dusecekOlanSilahinSpriteRenderer.sprite = dusmaninElindekiSilah.silahIcon;
            rastgeleDusenSilah.dayaniklilik = dusmaninElindekiSilah.silahDayanikliligi;
            Instantiate(dusecekOlanSilah, new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z), transform.rotation);
        }
    }
}
