using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptKontrol : MonoBehaviour
{
    public oyuncuAnimasyon oyuncuAnimasyon;
    public oyuncuEfektYoneticisi oyuncuEfektYoneticisi;
    public oyuncuHareket oyuncuHareket;
    public oyuncuSaldiriTest oyuncuSaldiriTest;
    public canKontrol canKontrol;
    public envanterKontrol envanterKontrol;
    public tirmanma tirmanma;
    public yetenekAgaciUI yetenekAgaciUI;
    public yetenekAgaclari yetenekAgaclari;
    public yetenekKontrol yetenekKontrol;
    public silahKontrol silahKontrol;
    public silahUltileri silahUltileri;
    public toplanabilirKullanmaScripti toplanabilirKullanmaScripti;
    public ozelEtkilerKontrol ozelEtkilerKontrol;
    public silahOzellikleriniGetir silah1OzellikleriniGetir, silah2OzellikleriniGetir;
    public ozelGucKullanmaScripti ozelGuc1KullanmaScripti, ozelGuc2KullanmaScripti;

    // -------------------------------------------------------------------------------------------------------------------------------------------------------

    public dusmanAgresif dusmanAgresif;
    public dusmanHareket dusmanHareket;
    public dusmanHasar dusmanHasar;
    public dusmanOk dusmanOk;
    public dusmanShuriken dusmanShuriken;
    public dusmanSpawn dusmanSpawn;
    public dusmanYumi dusmanYumi;
    public GameManager gameManager;
    public projectile projectile;
    public firlatmaTest firlatmaTest;
    public silahSecimi silah1Secimi, silah2Secimi; // silah1/2 - silahlarý listeden seçmeyi saðlar
    public DuraklatmaMenusu duraklatmaMenusu;
    public AnaMenu anaMenu;
    public merdiven merdiven;
    public sefPanelScripti sefPanelScripti;
    public dumendenDenemelerScripti dumendenDenemelerScripti;
    public tusAtamalariMenu tusAtamalariMenu;
    public tusDizilimiTest tusDizilimiTest;
    public tusDizilimleri tusDizilimleri;
    public LocalizationManager localizationManager;
    public localizedText localizedText;
    public uiEfektYoneticisi uiEfektYoneticisi;
    public sesKontrol sesKontrol;
    public toplanabilirSecmeScripti toplanabilirSecmeScripti; // sandiklar - sandiktan cikan iksiri belirler
    public yetenekObjesi yetenekObjesi; // yetenekler - her yeteneðin özelliðini tutar
    public ozelGucOzellikleri ozelGucOzellikleri; // ozel güçler - ozel guclerin ozelliklerini tutar
    public silahOzellikleri silahOzellikleri; // silahlar - silahlarýn özelliklerini tutar
    public sandikKirma sandikKirma; // sandiklar - sandiklarin silahlarla kirilmasini saglar
    public toplanabilirOzellikleri toplanabilirOzellikleri; // iksirler - iksirlerin ozelliklerini tutar
    public rastgeleDusenSilah rastgeleDusenSilah; // dusmandan dusen silahlar - dusmandan dusen silah özelliklerini tutar
    public rastgeleSilahDusurmeScripti rastgeleSilahDusurmeScripti; // silahlarýn rastgele düsmesini saglar
    public aniAgaciAcma aniAgaciAcma;
    public aniAgaciEfektleri aniAgaciEfektleri;
    public aniPuaniScripti aniPuaniScripti;
    public silahciPanelScripti silahciPanelScripti;






    void Start()
    {

    }

    void Update()
    {

    }
}
