using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirimOlusturucu : MonoBehaviour
{
    public GameObject baslangicBirimi;
    public GameObject bitisBirimi;
    public GameObject gecisBirimi1, gecisBirimi2;

    // Bu listede rastgele birimlerin prefab'leri olacak
    public List<GameObject> rastgeleBirimPrefabListesi;
    public List<GameObject> kullanilanRastgeleBirimler = new List<GameObject>();

    public List<GameObject> rastgeleAraBirimPrefabListesi;
    public List<GameObject> kullanilanRastgeleAraBirimler = new List<GameObject>();

    // Global değişkenler
    public GameObject birimOlusturucu; // Scriptin bağlı olduğu GameObject
    public GameObject baslangicBirim; // Şu anki birim (başlangıç birimi)
    public GameObject birinciRastgeleBirim;
    public GameObject ikinciRastgeleBirim;
    public GameObject birinciAraBirim;
    public GameObject birinciGecisBirim;
    public GameObject ucuncuRastgeleBirim;
    public GameObject dorduncuRastgeleBirim;
    public GameObject ikinciAraBirim;
    public GameObject ikinciGecisBirim;
    public GameObject besinciRastgeleBirim;
    public GameObject altinciRastgeleBirim;
    public GameObject yedinciRastgeleBirim;
    public GameObject bitis;

    void Awake()
    {
        Debug.Log(gameObject.name + " <==> birim oluşturucu");

        // Scriptin bağlı olduğu GameObject'ı referans al
        birimOlusturucu = gameObject;

        // Başlangıç birimini oluştur ve kök olarak tanımla
        baslangicBirim = Instantiate(baslangicBirimi, Vector3.zero, Quaternion.identity, birimOlusturucu.transform);

        // Birinci rastgele birimi oluştur
        birinciRastgeleBirim = OlusturVeKonumla("birim", baslangicBirim, 27);

        // İkinci rastgele birimi oluştur
        ikinciRastgeleBirim = OlusturVeKonumla("birim", birinciRastgeleBirim, 36);

        // Birinci ara birimi oluştur
        birinciAraBirim = OlusturVeKonumla("araBirim", ikinciRastgeleBirim, 27);

        // Birinci gecis birimi oluştur
        birinciGecisBirim = Instantiate(gecisBirimi1, birinciAraBirim.transform.position + Vector3.right * 18, Quaternion.identity, birimOlusturucu.transform);

        // Üçüncü rastgele birimi oluştur
        ucuncuRastgeleBirim = OlusturVeKonumla("birim", birinciGecisBirim, 27);

        // Dördüncü rastgele birimi oluştur
        dorduncuRastgeleBirim = OlusturVeKonumla("birim", ucuncuRastgeleBirim, 36);

        // İkinci ara birimi oluştur
        ikinciAraBirim = OlusturVeKonumla("araBirim", dorduncuRastgeleBirim, 27);

        // İkinci gecis birimi oluştur
        ikinciGecisBirim = Instantiate(gecisBirimi2, ikinciAraBirim.transform.position + Vector3.right * 18, Quaternion.identity, birimOlusturucu.transform);

        // Beşinci rastgele birimi oluştur
        besinciRastgeleBirim = OlusturVeKonumla("birim", ikinciGecisBirim, 27);

        // Altıncı rastgele birimi oluştur
        altinciRastgeleBirim = OlusturVeKonumla("birim", besinciRastgeleBirim, 36);

        // Yedinci rastgele birimi oluştur
        yedinciRastgeleBirim = OlusturVeKonumla("birim", altinciRastgeleBirim, 36);

        // Son birimi oluştur
        bitis = Instantiate(bitisBirimi, yedinciRastgeleBirim.transform.position + Vector3.right * 27, Quaternion.identity, birimOlusturucu.transform);
    }

    GameObject OlusturVeKonumla(string hangiBirim, GameObject parentObject, float xOffset)
    {
        if (hangiBirim == "birim")
        {
            GameObject yeniBirim;
            // Kullanılmayan rastgele birim prefab'ini seç
            GameObject randomPrefab = randomBirimGetir();
            kullanilanRastgeleBirimler.Add(randomPrefab); // Kullanılan rastgele birimleri listeye ekle
            yeniBirim = Instantiate(randomPrefab, parentObject.transform.position + Vector3.right * xOffset, Quaternion.identity, birimOlusturucu.transform);
            return yeniBirim;
        }
        else if (hangiBirim == "araBirim")
        {
            GameObject yeniBirim;
            // Kullanılmayan rastgele ara birim prefab'ini seç
            GameObject randomPrefab = randomAraBirimGetir();
            kullanilanRastgeleAraBirimler.Add(randomPrefab); // Kullanılan rastgele birimleri listeye ekle
            yeniBirim = Instantiate(randomPrefab, parentObject.transform.position + Vector3.right * xOffset, Quaternion.identity, birimOlusturucu.transform);
            return yeniBirim;
        }

        // Eğer hangiBirim ne "birim" ne de "araBirim" ise, null döndür
        return null;
    }

    GameObject randomBirimGetir()
    {
        // Kullanılmayan rastgele birim prefab'ini seç
        GameObject randomPrefab = rastgeleBirimPrefabListesi[Random.Range(0, rastgeleBirimPrefabListesi.Count)];

        // Seçilen birim prefab'i listeden kaldır
        rastgeleBirimPrefabListesi.Remove(randomPrefab);

        return randomPrefab;
    }
    GameObject randomAraBirimGetir()
    {
        // Kullanılmayan rastgele ara birim prefab'ini seç
        GameObject randomPrefab = rastgeleAraBirimPrefabListesi[Random.Range(0, rastgeleAraBirimPrefabListesi.Count)];

        // Seçilen ara birim prefab'ini listeden kaldır
        rastgeleAraBirimPrefabListesi.Remove(randomPrefab);

        return randomPrefab;
    }
}
