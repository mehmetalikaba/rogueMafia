using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirimOlusturucu : MonoBehaviour
{
    public GameObject baslangicPrefab;
    public GameObject araPrefab;
    public GameObject sonPrefab;
    public GameObject[] rastgeleBirimPrefablari;

    void Start()
    {
        // Başlangıç birimini oluştur
        GameObject baslangicBirimi = Instantiate(baslangicPrefab, Vector3.zero, Quaternion.identity);

        // Başlangıç biriminin sağ tarafına 1 rastgele birim ekleyelim
        Vector3 pozisyon1 = baslangicBirimi.transform.position + new Vector3(25f, 0f, 0f);
        GameObject rastgeleBirim1 = RastgeleBirimSec();
        Instantiate(rastgeleBirim1, pozisyon1, Quaternion.identity);

        // 36 birimlik boşluk bırakalım
        Vector3 pozisyon2 = pozisyon1 + new Vector3(36f, 0f, 0f);

        // Başlangıç biriminin sağ tarafına 1 daha rastgele birim ekleyelim
        GameObject rastgeleBirim2 = RastgeleBirimSec();
        Instantiate(rastgeleBirim2, pozisyon2, Quaternion.identity);

        // 36 birimlik boşluk bırakalım
        Vector3 pozisyon3 = pozisyon2 + new Vector3(25f, 0f, 0f);

        // Ara birimi oluştur
        GameObject araBirimi1 = Instantiate(araPrefab, pozisyon3, Quaternion.identity);

        // Ara biriminin sağ tarafına 1 rastgele birim ekleyelim
        Vector3 pozisyon4 = araBirimi1.transform.position + new Vector3(25f, 0f, 0f);
        GameObject rastgeleBirim3 = RastgeleBirimSec();
        Instantiate(rastgeleBirim3, pozisyon4, Quaternion.identity);

        // 36 birimlik boşluk bırakalım
        Vector3 pozisyon5 = pozisyon4 + new Vector3(36f, 0f, 0f);

        // Ara biriminin sağ tarafına 1 daha rastgele birim ekleyelim
        GameObject rastgeleBirim4 = RastgeleBirimSec();
        Instantiate(rastgeleBirim4, pozisyon5, Quaternion.identity);

        // 36 birimlik boşluk bırakalım
        Vector3 pozisyon6 = pozisyon5 + new Vector3(25f, 0f, 0f);

        // Ara birimi oluştur
        GameObject araBirimi2 = Instantiate(araPrefab, pozisyon6, Quaternion.identity);

        // Ara biriminin sağ tarafına 3 rastgele birim ekleyelim
        for (int i = 0; i < 3; i++)
        {
            Vector3 pozisyon = araBirimi2.transform.position + new Vector3(28f * (i + 1), 0f, 0f);
            GameObject rastgeleBirim = RastgeleBirimSec();
            Instantiate(rastgeleBirim, pozisyon, Quaternion.identity);
        }

        // Son birimi oluştur
        Vector3 pozisyon7 = araBirimi2.transform.position + new Vector3(25f * 4, 0f, 0f);
        GameObject sonBirimi = Instantiate(sonPrefab, pozisyon7, Quaternion.identity);
    }

    // Rastgele bir birim seçmek için fonksiyon
    GameObject RastgeleBirimSec()
    {
        int randomIndex = Random.Range(0, rastgeleBirimPrefablari.Length);
        GameObject secilenPrefab = rastgeleBirimPrefablari[randomIndex];
        return secilenPrefab;
    }
}
