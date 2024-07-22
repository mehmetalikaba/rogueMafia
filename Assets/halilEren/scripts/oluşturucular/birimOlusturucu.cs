using UnityEngine;
using System.Collections.Generic;

public class birimOlusturcu : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Sahneye eklemek için farklı prefab objeleri tanımlayın

    private List<GameObject> availableObjects = new List<GameObject>(); // Kullanılabilir objelerin listesi

    void Start()
    {
        InitializeAvailableObjects();
        SpawnObjects();
    }

    void InitializeAvailableObjects()
    {
        // Kullanılabilir objeler listesini başlangıçta doldurun
        availableObjects.AddRange(objectPrefabs);
    }

    void SpawnObjects()
    {
        int numberOfObjects = availableObjects.Count;

        // Kullanılabilir obje sayısı kadar döngü yapın
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Rastgele bir obje seçin
            int randomIndex = Random.Range(0, availableObjects.Count);
            GameObject selectedPrefab = availableObjects[randomIndex];

            // Rastgele bir pozisyon oluşturun (x koordinatı her obje için farklı olacak şekilde)
            Vector3 spawnPosition = new Vector3(i * 35, 0, 0f); // x koordinatını 2 birim aralıklarla arttırarak oluştur

            // Seçilen prefab'ı rastgele pozisyonda ve varsayılan rotasyonu ile oluşturun
            GameObject newObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

            // Oluşturulan objeyi bu script'e bağlı olan GameObject altında hiyerarşiye ekleyin (opsiyonel)
            newObject.transform.parent = transform;

            // Kullanılan objeyi listeden kaldırın
            availableObjects.RemoveAt(randomIndex);
        }
    }
}
