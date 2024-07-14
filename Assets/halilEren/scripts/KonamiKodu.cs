using UnityEngine;

public class KonamiKodu : MonoBehaviour
{
    public Transform target;
    // Konami kodu sırası
    private readonly KeyCode[] konamiCode = {
        KeyCode.UpArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.B,
        KeyCode.A
    };

    private int currentIndex = 0;

    void Update()
    {
        // Her güncellemede kontrol ederiz
        if (Input.anyKeyDown)
        {
            // Basılan tuşu kontrol ederiz
            if (Input.GetKeyDown(konamiCode[currentIndex]))
            {
                currentIndex++;

                // Eğer tüm kod doğru girildiyse istediğimiz işlemi yapabiliriz
                if (currentIndex == konamiCode.Length)
                {
                    ActivateKonamiCode();
                    currentIndex = 0; // Sıfırlarız, yeniden başlamak için
                }
            }
            else
            {
                currentIndex = 0; // Yanlış tuşa basılırsa sıfırlarız
            }
        }
    }

    void ActivateKonamiCode()
    {
        Debug.Log("KONAMI KODU GİRİLDİ");
        target.transform.localScale= new Vector2(target.transform.localScale.x*2,target.transform.localScale.y*2);
    }
}