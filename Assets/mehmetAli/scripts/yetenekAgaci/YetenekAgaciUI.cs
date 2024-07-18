using UnityEngine;
using UnityEngine.UI;

public class yetenekAgaciUI : MonoBehaviour
{
    public Text[] yetenekAdlari;
    public Text[] yetenekAciklamalari;
    public Button[] yetenekButonlari;
    public yetenekObjesi[] yetenekler;
    public yetenekAgaclari yetenekAgaci;
    public envanterKontrol envanterKontrol;
    public GameObject yetenekAgaciPaneli;
    public yetenekKontrol yetenekKontrol;

    private void Start()
    {
        envanterKontrol = FindObjectOfType<envanterKontrol>();
        yetenekKontrol = GetComponent<yetenekKontrol>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num4Tusu")))
        {
            if (!yetenekAgaciPaneli.activeSelf)
            {
                yetenekAgaciPaneli.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
            else if (yetenekAgaciPaneli.activeSelf)
            {
                yetenekAgaciPaneli.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
            }
        }
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num7Tusu")))
        {
            yetenekKontrol.menzilliYeteneklerListesi[0].yetenekSeviyesi = 0;
            yetenekKontrol.menzilliYeteneklerListesi[1].yetenekSeviyesi = 0;
            yetenekKontrol.menzilliYeteneklerListesi[2].yetenekSeviyesi = 0;
            yetenekKontrol.pasifYeteneklerListesi[0].yetenekSeviyesi = 0;
            yetenekKontrol.pasifYeteneklerListesi[1].yetenekSeviyesi = 0;
            yetenekKontrol.pasifYeteneklerListesi[2].yetenekSeviyesi = 0;
            yetenekKontrol.yakinYeteneklerListesi[0].yetenekSeviyesi = 0;
            yetenekKontrol.yakinYeteneklerListesi[1].yetenekSeviyesi = 0;
            yetenekKontrol.yakinYeteneklerListesi[2].yetenekSeviyesi = 0;
        }
    }

    public void menzilli1SeviyeYukseltme()
    {
        if (yetenekKontrol.menzilliYeteneklerListesi[0].gerekliAniPuani <= envanterKontrol.anilar)
        {
            yetenekKontrol.menzilliYeteneklerListesi[0].yetenekSeviyesi = 1;
            envanterKontrol.anilar -= yetenekKontrol.menzilliYeteneklerListesi[0].gerekliAniPuani;
            Debug.Log("menzilli yetenek yükseldi kilitler acildi");
        }
        else
            Debug.Log("yeteri ani puani yok");
    }
    public void menzilli2SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekKontrol.menzilliYeteneklerListesi[1].gerekliAniPuani && yetenekKontrol.menzilliYeteneklerListesi[0].yetenekSeviyesi == 1)
        {
            if (yetenekKontrol.menzilliYeteneklerListesi[1].yetenekSeviyesi < yetenekKontrol.menzilliYeteneklerListesi[1].maxSeviye)
            {
                yetenekKontrol.menzilliYeteneklerListesi[1].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekKontrol.menzilliYeteneklerListesi[1].gerekliAniPuani;
                Debug.Log("pasif2 yetenek seviyesi");
            }
            else
                Debug.Log("yetenek seviyesi max seviyeye ulasti");
        }
        else
            Debug.Log("yeteri ani puani yok veya baslangic yetenegi acilmadi");
    }
    public void menzilli3SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekKontrol.menzilliYeteneklerListesi[2].gerekliAniPuani && yetenekKontrol.menzilliYeteneklerListesi[0].yetenekSeviyesi == 1)
        {
            if (yetenekKontrol.menzilliYeteneklerListesi[2].yetenekSeviyesi < yetenekKontrol.menzilliYeteneklerListesi[2].maxSeviye)
            {
                yetenekKontrol.menzilliYeteneklerListesi[2].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekKontrol.menzilliYeteneklerListesi[2].gerekliAniPuani;
                Debug.Log("menzilli3 yetenek seviyesi");
            }
            else
                Debug.Log("yetenek seviyesi max seviyeye ulasti");
        }
        else
            Debug.Log("yeteri ani puani yok veya baslangic yetenegi acilmadi");
    }
    public void pasif1SeviyeYukseltme()
    {
        if (yetenekKontrol.pasifYeteneklerListesi[0].gerekliAniPuani <= envanterKontrol.anilar)
        {
            yetenekKontrol.pasifYeteneklerListesi[0].yetenekSeviyesi = 1;
            envanterKontrol.anilar -= yetenekKontrol.pasifYeteneklerListesi[0].gerekliAniPuani;
            Debug.Log("pasif yetenek yükseldi kilitler acildi");
        }
        else
            Debug.Log("yeteri ani puani yok");
    }
    public void pasif2SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekKontrol.pasifYeteneklerListesi[1].gerekliAniPuani && yetenekKontrol.pasifYeteneklerListesi[0].yetenekSeviyesi == 1)
        {
            if (yetenekKontrol.pasifYeteneklerListesi[1].yetenekSeviyesi < yetenekKontrol.pasifYeteneklerListesi[1].maxSeviye)
            {
                yetenekKontrol.pasifYeteneklerListesi[1].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekKontrol.pasifYeteneklerListesi[1].gerekliAniPuani;
                Debug.Log("pasif2 yetenek seviyesi");
            }
            else
                Debug.Log("yetenek seviyesi max seviyeye ulasti");
        }
        else
            Debug.Log("yeteri ani puani yok veya baslangic yetenegi acilmadi");
    }
    public void pasif3SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekKontrol.pasifYeteneklerListesi[2].gerekliAniPuani && yetenekKontrol.pasifYeteneklerListesi[0].yetenekSeviyesi == 1)
        {
            if (yetenekKontrol.pasifYeteneklerListesi[2].yetenekSeviyesi < yetenekKontrol.pasifYeteneklerListesi[2].maxSeviye)
            {
                yetenekKontrol.pasifYeteneklerListesi[2].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekKontrol.pasifYeteneklerListesi[2].gerekliAniPuani;
                Debug.Log("pasif3 yetenek seviyesi");
            }
            else
                Debug.Log("yetenek seviyesi max seviyeye ulasti");
        }
        else
            Debug.Log("yeteri ani puani yok veya baslangic yetenegi acilmadi");
    }
    public void yakin1SeviyeYukseltme()
    {
        if (yetenekKontrol.yakinYeteneklerListesi[0].gerekliAniPuani <= envanterKontrol.anilar)
        {
            yetenekKontrol.yakinYeteneklerListesi[0].yetenekSeviyesi = 1;
            envanterKontrol.anilar -= yetenekKontrol.yakinYeteneklerListesi[0].gerekliAniPuani;
            Debug.Log("yakin yetenek yükseldi kilitler acildi");
        }
        else
            Debug.Log("yeteri ani puani yok");
    }
    public void yakin2SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekKontrol.yakinYeteneklerListesi[1].gerekliAniPuani && yetenekKontrol.yakinYeteneklerListesi[0].yetenekSeviyesi == 1)
        {
            if (yetenekKontrol.yakinYeteneklerListesi[1].yetenekSeviyesi < yetenekKontrol.yakinYeteneklerListesi[1].maxSeviye)
            {
                yetenekKontrol.yakinYeteneklerListesi[1].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekKontrol.yakinYeteneklerListesi[1].gerekliAniPuani;
                Debug.Log("yakin2 yetenek seviyesi");
            }
            else
                Debug.Log("yetenek seviyesi max seviyeye ulasti");
        }
        else
            Debug.Log("yeteri ani puani yok veya baslangic yetenegi acilmadi");
    }
    public void yakin3SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekKontrol.yakinYeteneklerListesi[2].gerekliAniPuani && yetenekKontrol.yakinYeteneklerListesi[0].yetenekSeviyesi == 1)
        {
            if (yetenekKontrol.yakinYeteneklerListesi[2].yetenekSeviyesi < yetenekKontrol.yakinYeteneklerListesi[2].maxSeviye)
            {
                yetenekKontrol.yakinYeteneklerListesi[2].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekKontrol.yakinYeteneklerListesi[2].gerekliAniPuani;
                Debug.Log("yakin3 yetenek seviyesi");
            }
            else
                Debug.Log("yetenek seviyesi max seviyeye ulasti");
        }
        else
            Debug.Log("yeteri ani puani yok veya baslangic yetenegi acilmadi");
    }
}
