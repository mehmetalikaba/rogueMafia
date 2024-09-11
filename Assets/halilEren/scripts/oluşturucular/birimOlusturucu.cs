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
        // Scriptin bağlı olduğu GameObject'ı referans al
        GameObject parentObject = gameObject;

        // Başlangıç birimini oluştur ve kök olarak tanımla
        GameObject currentUnit = Instantiate(baslangicBirimi, Vector3.zero, Quaternion.identity, parentObject.transform);
        currentUnit.name = "CurrentUnit"; // İsim vermek isteyebilirsiniz

        // Birinci rastgele birimi oluştur
        GameObject birinciRastgeleBirim = OlusturVeKonumla(currentUnit, 27);

        // İkinci rastgele birimi oluştur
        GameObject ikinciRastgeleBirim = OlusturVeKonumla(birinciRastgeleBirim, 36);

        // Birinci ara birimi oluştur
        GameObject birinciAraBirim = OlusturVeKonumla(ikinciRastgeleBirim, 27, araBirimi);

        // Üçüncü rastgele birimi oluştur
        GameObject ucuncuRastgeleBirim = OlusturVeKonumla(birinciAraBirim, 27);

        // Dördüncü rastgele birimi oluştur
        GameObject dorduncuRastgeleBirim = OlusturVeKonumla(ucuncuRastgeleBirim, 36);

        // İkinci ara birimi oluştur
        GameObject ikinciAraBirim = OlusturVeKonumla(dorduncuRastgeleBirim, 27, araBirimi1);

        // Beşinci rastgele birimi oluştur
        GameObject besinciRastgeleBirim = OlusturVeKonumla(ikinciAraBirim, 27);

        // Altıncı rastgele birimi oluştur
        GameObject altinciRastgeleBirim = OlusturVeKonumla(besinciRastgeleBirim, 36);

        // Yedinci rastgele birimi oluştur
        GameObject yedinciRastgeleBirim = OlusturVeKonumla(altinciRastgeleBirim, 36);

        // Son birimi oluştur
        //ameObject bitis = Instantiate(bitisBirimi, yedinciRastgeleBirim.transform.position + Vector3.right * 27, Quaternion.identity, parentObject.transform);
    }

    GameObject OlusturVeKonumla(GameObject parentObject, float xOffset, GameObject birimPrefab = null)
    {
        GameObject yeniBirim;
        if (birimPrefab == null)
        {
            // Kullanılmayan rastgele birim prefab'ini seç
            GameObject randomPrefab = GetRandomPrefab();
            kullanilanRastgeleBirimler.Add(randomPrefab); // Kullanılan rastgele birimleri listeye ekle
            yeniBirim = Instantiate(randomPrefab, parentObject.transform.position + Vector3.right * xOffset, Quaternion.identity, parentObject.transform);
        }
        else
        {
            yeniBirim = Instantiate(birimPrefab, parentObject.transform.position + Vector3.right * xOffset, Quaternion.identity, parentObject.transform);
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
