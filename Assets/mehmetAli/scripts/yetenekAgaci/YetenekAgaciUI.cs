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
    public aniAgaciEfektleri aniAgaciEfektleri;

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
            Debug.Log("skiller sifirlandi");
            foreach (var yetenek in yetenekKontrol.menzilliYeteneklerListesi)
            {
                yetenek.yetenekSeviyesi = 0;
                yetenek.oyunaUygulandi = false;
            }
            foreach (var yetenek in yetenekKontrol.pasifYeteneklerListesi)
            {
                yetenek.yetenekSeviyesi = 0;
                yetenek.oyunaUygulandi = false;
            }
            foreach (var yetenek in yetenekKontrol.yakinYeteneklerListesi)
            {
                yetenek.yetenekSeviyesi = 0;
                yetenek.oyunaUygulandi = false;
            }
            yetenekKontrol.normalleriGetirme();
        }
    }

    public void menzilli1SeviyeYukseltme()
    {
        if (yetenekKontrol.menzilliYeteneklerListesi[0].gerekliAniPuani <= envanterKontrol.anilar)
        {
            yetenekKontrol.menzilliYeteneklerListesi[0].yetenekSeviyesi = 1;
            envanterKontrol.anilar -= yetenekKontrol.menzilliYeteneklerListesi[0].gerekliAniPuani;
            envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
            Debug.Log("menzilli yetenek yükseldi kilitler acildi");
            aniAgaciEfektleri = yetenekButonlari[0].GetComponent<aniAgaciEfektleri>();
            aniAgaciEfektleri.gelistirilmis();
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
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                Debug.Log("pasif2 yetenek seviyesi");
                aniAgaciEfektleri = yetenekButonlari[1].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
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
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                Debug.Log("menzilli3 yetenek seviyesi");
                aniAgaciEfektleri = yetenekButonlari[2].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
            }
            else
                Debug.Log("yetenek seviyesi max seviyeye ulasti");
        }
        else
            Debug.Log("yeteri ani puani yok veya baslangic yetenegi acilmadi");
    }
    public void pasif1SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekKontrol.pasifYeteneklerListesi[0].gerekliAniPuani)
        {
            if (yetenekKontrol.pasifYeteneklerListesi[0].yetenekSeviyesi < yetenekKontrol.pasifYeteneklerListesi[0].maxSeviye)
            {
                yetenekKontrol.pasifYeteneklerListesi[0].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekKontrol.pasifYeteneklerListesi[0].gerekliAniPuani;
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                Debug.Log("pasif1 yetenek seviyesi");
                aniAgaciEfektleri = yetenekButonlari[3].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
            }
            else
                Debug.Log("yetenek seviyesi max seviyeye ulasti");
        }
        else
            Debug.Log("yeteri ani puani yok veya baslangic yetenegi acilmadi");
    }
    public void pasif2SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekKontrol.pasifYeteneklerListesi[1].gerekliAniPuani && yetenekKontrol.pasifYeteneklerListesi[0].yetenekSeviyesi > 0)
        {
            if (yetenekKontrol.pasifYeteneklerListesi[1].yetenekSeviyesi < yetenekKontrol.pasifYeteneklerListesi[1].maxSeviye)
            {
                yetenekKontrol.pasifYeteneklerListesi[1].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekKontrol.pasifYeteneklerListesi[1].gerekliAniPuani;
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                Debug.Log("pasif2 yetenek seviyesi");
                aniAgaciEfektleri = yetenekButonlari[4].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
            }
            else
                Debug.Log("yetenek seviyesi max seviyeye ulasti");
        }
        else
            Debug.Log("yeteri ani puani yok veya baslangic yetenegi acilmadi");
    }
    public void pasif3SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekKontrol.pasifYeteneklerListesi[2].gerekliAniPuani && yetenekKontrol.pasifYeteneklerListesi[0].yetenekSeviyesi > 0)
        {
            if (yetenekKontrol.pasifYeteneklerListesi[2].yetenekSeviyesi < yetenekKontrol.pasifYeteneklerListesi[2].maxSeviye)
            {
                yetenekKontrol.pasifYeteneklerListesi[2].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekKontrol.pasifYeteneklerListesi[2].gerekliAniPuani;
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                Debug.Log("pasif3 yetenek seviyesi");
                aniAgaciEfektleri = yetenekButonlari[5].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
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
            envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
            Debug.Log("yakin yetenek yükseldi kilitler acildi");
            aniAgaciEfektleri = yetenekButonlari[6].GetComponent<aniAgaciEfektleri>();
            aniAgaciEfektleri.gelistirilmis();
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
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                Debug.Log("yakin2 yetenek seviyesi");
                aniAgaciEfektleri = yetenekButonlari[7].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
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
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                Debug.Log("yakin3 yetenek seviyesi");
                aniAgaciEfektleri = yetenekButonlari[8].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
            }
            else
                Debug.Log("yetenek seviyesi max seviyeye ulasti");
        }
        else
            Debug.Log("yeteri ani puani yok veya baslangic yetenegi acilmadi");
    }
}
