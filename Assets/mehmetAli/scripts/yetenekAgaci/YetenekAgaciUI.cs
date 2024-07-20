using UnityEngine;
using UnityEngine.UI;

public class yetenekAgaciUI : MonoBehaviour
{
    public Button[] yetenekButonlari;
    public yetenekAgaclari yetenekAgaclari;
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
            yetenekAgaciRaporu();
        }

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("num7Tusu")))
        {
            Debug.Log("skiller sifirlandi");
            foreach (var yetenek in yetenekAgaclari.menzilliYetenekler)
            {
                yetenek.yetenekSeviyesi = 0;
                yetenek.oyunaUygulandi = false;
            }
            foreach (var yetenek in yetenekAgaclari.pasifYetenekler)
            {
                yetenek.yetenekSeviyesi = 0;
                yetenek.oyunaUygulandi = false;
            }
            foreach (var yetenek in yetenekAgaclari.yakinYetenekler)
            {
                yetenek.yetenekSeviyesi = 0;
                yetenek.oyunaUygulandi = false;
            }
            yetenekKontrol.normalleriGetirme();
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }

    public void yetenekAgaciRaporu()
    {
        if (yetenekAgaclari.menzilliYetenekler[0].yetenekSeviyesi == 1)
            Debug.Log("menzilli yetenek 1 ACÝK");
        if (yetenekAgaclari.menzilliYetenekler[1].yetenekSeviyesi == 1)
            Debug.Log("menzilli yetenek 2 ACÝK");
        if (yetenekAgaclari.menzilliYetenekler[2].yetenekSeviyesi == 1)
            Debug.Log("menzilli yetenek 3 ACÝK");

        if (yetenekAgaclari.yakinYetenekler[1].yetenekSeviyesi == 1)
            Debug.Log("yakin yetenek 1 ACÝK");
        if (yetenekAgaclari.yakinYetenekler[2].yetenekSeviyesi == 1)
            Debug.Log("yakin yetenek 2 ACÝK");
        if (yetenekAgaclari.yakinYetenekler[3].yetenekSeviyesi == 1)
            Debug.Log("yakin yetenek 3 ACÝK");

        if (yetenekAgaclari.pasifYetenekler[1].yetenekSeviyesi == 1)
            Debug.Log("pasif yetenek 1 ACÝK");
        if (yetenekAgaclari.pasifYetenekler[2].yetenekSeviyesi == 1)
            Debug.Log("pasif yetenek 1 ACÝK");
        if (yetenekAgaclari.pasifYetenekler[3].yetenekSeviyesi == 1)
            Debug.Log("pasif yetenek 1 ACÝK");
    }

    public void menzilli1SeviyeYukseltme()
    {
        if (yetenekAgaclari.menzilliYetenekler[0].gerekliAniPuani <= envanterKontrol.anilar)
        {
            yetenekAgaclari.menzilliYetenekler[0].yetenekSeviyesi = 1;
            envanterKontrol.anilar -= yetenekAgaclari.menzilliYetenekler[0].gerekliAniPuani;
            envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
            aniAgaciEfektleri = yetenekButonlari[0].GetComponent<aniAgaciEfektleri>();
            aniAgaciEfektleri.gelistirilmis();
        }
    }
    public void menzilli2SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekAgaclari.menzilliYetenekler[1].gerekliAniPuani && yetenekAgaclari.menzilliYetenekler[0].yetenekSeviyesi == 1)
        {
            if (yetenekAgaclari.menzilliYetenekler[1].yetenekSeviyesi < yetenekAgaclari.menzilliYetenekler[1].maxSeviye)
            {
                yetenekAgaclari.menzilliYetenekler[1].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekAgaclari.menzilliYetenekler[1].gerekliAniPuani;
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                aniAgaciEfektleri = yetenekButonlari[1].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
            }
        }
    }
    public void menzilli3SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekAgaclari.menzilliYetenekler[2].gerekliAniPuani && yetenekAgaclari.menzilliYetenekler[0].yetenekSeviyesi == 1)
        {
            if (yetenekAgaclari.menzilliYetenekler[2].yetenekSeviyesi < yetenekAgaclari.menzilliYetenekler[2].maxSeviye)
            {
                yetenekAgaclari.menzilliYetenekler[2].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekAgaclari.menzilliYetenekler[2].gerekliAniPuani;
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                aniAgaciEfektleri = yetenekButonlari[2].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
            }
        }
    }
    public void pasif1SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekAgaclari.pasifYetenekler[0].gerekliAniPuani)
        {
            if (yetenekAgaclari.pasifYetenekler[0].yetenekSeviyesi < yetenekAgaclari.pasifYetenekler[0].maxSeviye)
            {
                yetenekAgaclari.pasifYetenekler[0].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekAgaclari.pasifYetenekler[0].gerekliAniPuani;
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                aniAgaciEfektleri = yetenekButonlari[3].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
            }
        }
    }
    public void pasif2SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekAgaclari.pasifYetenekler[1].gerekliAniPuani && yetenekAgaclari.pasifYetenekler[0].yetenekSeviyesi > 0)
        {
            if (yetenekAgaclari.pasifYetenekler[1].yetenekSeviyesi < yetenekAgaclari.pasifYetenekler[1].maxSeviye)
            {
                yetenekAgaclari.pasifYetenekler[1].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekAgaclari.pasifYetenekler[1].gerekliAniPuani;
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                aniAgaciEfektleri = yetenekButonlari[4].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
            }
        }
    }
    public void pasif3SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekAgaclari.pasifYetenekler[2].gerekliAniPuani && yetenekAgaclari.pasifYetenekler[0].yetenekSeviyesi > 0)
        {
            if (yetenekAgaclari.pasifYetenekler[2].yetenekSeviyesi < yetenekAgaclari.pasifYetenekler[2].maxSeviye)
            {
                yetenekAgaclari.pasifYetenekler[2].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekAgaclari.pasifYetenekler[2].gerekliAniPuani;
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                aniAgaciEfektleri = yetenekButonlari[5].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
            }
        }
    }
    public void yakin1SeviyeYukseltme()
    {
        if (yetenekAgaclari.yakinYetenekler[0].gerekliAniPuani <= envanterKontrol.anilar)
        {
            yetenekAgaclari.yakinYetenekler[0].yetenekSeviyesi = 1;
            envanterKontrol.anilar -= yetenekAgaclari.yakinYetenekler[0].gerekliAniPuani;
            envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
            aniAgaciEfektleri = yetenekButonlari[6].GetComponent<aniAgaciEfektleri>();
            aniAgaciEfektleri.gelistirilmis();
        }
    }
    public void yakin2SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekAgaclari.yakinYetenekler[1].gerekliAniPuani && yetenekAgaclari.yakinYetenekler[0].yetenekSeviyesi == 1)
        {
            if (yetenekAgaclari.yakinYetenekler[1].yetenekSeviyesi < yetenekAgaclari.yakinYetenekler[1].maxSeviye)
            {
                yetenekAgaclari.yakinYetenekler[1].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekAgaclari.yakinYetenekler[1].gerekliAniPuani;
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                aniAgaciEfektleri = yetenekButonlari[7].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
            }
        }
    }
    public void yakin3SeviyeYukseltme()
    {
        if (envanterKontrol.anilar >= yetenekAgaclari.yakinYetenekler[2].gerekliAniPuani && yetenekAgaclari.yakinYetenekler[0].yetenekSeviyesi == 1)
        {
            if (yetenekAgaclari.yakinYetenekler[2].yetenekSeviyesi < yetenekAgaclari.yakinYetenekler[2].maxSeviye)
            {
                yetenekAgaclari.yakinYetenekler[2].yetenekSeviyesi++;
                envanterKontrol.anilar -= yetenekAgaclari.yakinYetenekler[2].gerekliAniPuani;
                envanterKontrol.aniPuani.text = envanterKontrol.anilar.ToString("F0");
                aniAgaciEfektleri = yetenekButonlari[8].GetComponent<aniAgaciEfektleri>();
                aniAgaciEfektleri.gelistirilmis();
            }
        }
    }
}
