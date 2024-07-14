using UnityEngine;
using UnityEngine.UI;

public class YetenekUI : MonoBehaviour
{
    public Image ikon;
    public Text aciklama;
    public Button acmaButonu;
    private Yetenek yetenek;
    private OyuncuYetenekleri oyuncuYetenekleri;

    public void Setup(Yetenek yetenek)
    {
        this.yetenek = yetenek;
        ikon.sprite = yetenek.ikon;
        aciklama.text = yetenek.aciklama;
        acmaButonu.onClick.AddListener(YetenekAc);
        oyuncuYetenekleri = FindObjectOfType<OyuncuYetenekleri>();
    }

    private void YetenekAc()
    {
        if (oyuncuYetenekleri.YetenekAc(yetenek))
        {
            // Yetenek baþarýyla açýldý
            acmaButonu.interactable = false;
        }
        else
        {
            // Yetenek açýlma koþullarý saðlanamadý
            Debug.Log("Yetenek açýlma koþullarý saðlanamadý veya yeterli ejderha puaný yok.");
        }
    }
}
