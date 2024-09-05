using UnityEngine;
using TMPro;

public class sayac : MonoBehaviour
{
    public TextMeshProUGUI zamanText; // UI Text nesnesi, zamanı gösterecek

    private float zaman = 0f;

    void Update()
    {
        zaman += Time.deltaTime; // Zamanı güncelle
                                 // Zamanı UI Text nesnesine yazdır, format olarak dakika:saniye:yarım saniye şeklinde
        zamanText.text = string.Format("{0}:{1:00}", Mathf.Floor(zaman / 60), Mathf.Floor(zaman) % 60);
    }
}
