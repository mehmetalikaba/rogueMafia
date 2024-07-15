using TMPro;
using UnityEngine;

public class toplanabilirOzellikleri : MonoBehaviour
{
    public Sprite toplanabilirIcon;
    public string toplanabilirAdi, toplanabilirAciklamaKeyi;
    public bool oyuncuYakin;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;
    public GameObject iksirAdiObjesi;
    public TextMeshProUGUI iksirAdi;

    void Start()
    {
        toplanabilirKullanmaScripti = FindObjectOfType<toplanabilirKullanmaScripti>();
        iksirAdi.text = toplanabilirAdi;
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && oyuncuYakin)
        {
            toplanabilirKullanmaScripti.toplanabilirObje = gameObject;
            toplanabilirKullanmaScripti.toplanabilirObjeOzellikleriniGetir();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
        {
            iksirAdiObjesi.SetActive(true);
            oyuncuYakin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("oyuncu"))
        {
            iksirAdiObjesi.SetActive(false);
            oyuncuYakin = false;
        }
    }
}