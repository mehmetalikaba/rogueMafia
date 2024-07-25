using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirimOlusturucu : MonoBehaviour
{
    public GameObject baslangicBirimi;
    public GameObject araBirimi;
    public GameObject araBirimi1;
    public GameObject bitisBirimi;

    // Bu listede rastgele birimlerin prefab'leri olacak
    public List<GameObject> rastgeleBirimPrefabListesi;

    private List<GameObject> kullanilanRastgeleBirimler = new List<GameObject>();

    void Start()
    {
        // Başlangıç birimini oluştur
        GameObject currentUnit = Instantiate(baslangicBirimi, Vector3.zero, Quaternion.identity);

        // Birinci rastgele birim
        GameObject birinciRastgeleBirim = OlusturVeKonumla(currentUnit, 27);

        // İkinci rastgele birim
        GameObject ikinciRastgeleBirim = OlusturVeKonumla(birinciRastgeleBirim, 36);

        // Birinci ara birim
        GameObject birinciAraBirim = OlusturVeKonumla(ikinciRastgeleBirim, 27, araBirimi);

        // Üçüncü rastgele birim
        GameObject ucuncuRastgeleBirim = OlusturVeKonumla(birinciAraBirim, 27);

        // Dördüncü rastgele birim
        GameObject dorduncuRastgeleBirim = OlusturVeKonumla(ucuncuRastgeleBirim, 36);

        // İkinci ara birim
        GameObject ikinciAraBirim = OlusturVeKonumla(dorduncuRastgeleBirim, 27, araBirimi1);

        // Beşinci rastgele birim
        GameObject besinciRastgeleBirim = OlusturVeKonumla(ikinciAraBirim, 27);

        // Altıncı rastgele birim
        GameObject altinciRastgeleBirim = OlusturVeKonumla(besinciRastgeleBirim, 36);

        // Yedinci rastgele birim
        GameObject yedinciRastgeleBirim = OlusturVeKonumla(altinciRastgeleBirim, 36);

        // Son birim
        Instantiate(bitisBirimi, yedinciRastgeleBirim.transform.position + Vector3.right * 27, Quaternion.identity);
    }

    GameObject OlusturVeKonumla(GameObject parentObject, float xOffset, GameObject birimPrefab = null)
    {
        GameObject yeniBirim;
        if (birimPrefab == null)
        {
            // Kullanılmayan rastgele birim prefab'ini seç
            GameObject randomPrefab = GetRandomPrefab();
            kullanilanRastgeleBirimler.Add(randomPrefab); // Kullanılan rastgele birimleri listeye ekle
            yeniBirim = Instantiate(randomPrefab, parentObject.transform.position + Vector3.right * xOffset, Quaternion.identity);
        }
        else
        {
            yeniBirim = Instantiate(birimPrefab, parentObject.transform.position + Vector3.right * xOffset, Quaternion.identity);
        }
        return yeniBirim;
    }

    GameObject GetRandomPrefab()
    {
        // Kullanılmayan rastgele birim prefab'ini seç
        GameObject randomPrefab = rastgeleBirimPrefabListesi[Random.Range(0, rastgeleBirimPrefabListesi.Count)];

        // Seçilen prefab'i listeden kaldır
        rastgeleBirimPrefabListesi.Remove(randomPrefab);

        return randomPrefab;
    }
}
