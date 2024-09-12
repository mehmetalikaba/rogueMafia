using UnityEngine;
using UnityEngine.UI;

public class shifuPanelScripti : MonoBehaviour
{
    public GameObject daireButon;
    public bool oyuncuYakin, etkilesimKilitli;
    public GameObject oyunPaneli, shifuPanel;

    public DuraklatmaMenusu duraklatmaMenusu;
    public yetenekKontrol yetenekKontrol;
    public kaydetKontrolYetenek kaydetKontrolYetenek;
    public oyuncuHareket oyuncuHareket;


    public void Start()
    {
        duraklatmaMenusu = FindObjectOfType<DuraklatmaMenusu>();
        oyuncuHareket = FindObjectOfType<oyuncuHareket>();
    }

    void Update()
    {
        oyuncuYakin = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Oyuncu"));
        if (oyuncuYakin)
            daireButon.SetActive(true);
        else if (!oyuncuYakin)
            daireButon.SetActive(false);

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")) && !etkilesimKilitli)
        {
            if (oyuncuYakin && !shifuPanel.activeSelf)
                durdur();
            else if (oyuncuYakin && shifuPanel.activeSelf)
                devamEt();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton2) && !etkilesimKilitli)
        {
            if (oyuncuYakin && !shifuPanel.activeSelf)
                durdur();
            else if (oyuncuYakin && shifuPanel.activeSelf)
                devamEt();
        }
    }

    public void durdur()
    {
        kaydetKontrolYetenek.jsonYetenekYukle();
        //duraklatmaMenusu.duraklatmaKilitli = true;
        oyuncuHareket.hareketKilitli = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        shifuPanel.SetActive(true);
        oyunPaneli.SetActive(false);
    }
    public void devamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        shifuPanel.SetActive(false);
        oyunPaneli.SetActive(true);
        //duraklatmaMenusu.duraklatmaKilitli = false;
        oyuncuHareket.hareketKilitli = false;
    }
}