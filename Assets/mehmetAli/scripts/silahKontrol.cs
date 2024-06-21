using UnityEngine;
using UnityEngine.UI;

public class silahKontrol : MonoBehaviour
{
    public silahTest silah1Test;
    public silahTest silah2Test;

    public GameObject silah1, silah2;

    public string seciliSilahTuru;
    public string seciliSilahAdi;
    public float seciliSilahSaldiriHasari;
    public float seciliSilahSaldiriMenzili;
    public RuntimeAnimatorController seciliSilahKarakterAnimator;

    public string seciliSilahTuru2;
    public string seciliSilahAdi2;
    public float seciliSilahSaldiriHasari2;
    public float seciliSilahSaldiriMenzili2;
    public RuntimeAnimatorController seciliSilahKarakterAnimator2;

    public Image seciliSilah1Image;
    public Image seciliSilah2Image;

    public SpriteRenderer silah1SpriteRenderer;
    public SpriteRenderer silah2SpriteRenderer;

    public bool silah1Secili;
    public bool silah2Secili;

    void Start()
    {
        silah1Secili = true;

        silah1Test = silah1.GetComponent<silahTest>();
        silah2Test = silah2.GetComponent<silahTest>();

        silah1SpriteRenderer = silah1.gameObject.GetComponent<SpriteRenderer>();
        silah2SpriteRenderer = silah2.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        seciliSilahTuru = silah1Test.silahTuru;
        seciliSilahAdi = silah1Test.silahAdi;
        seciliSilahSaldiriHasari = silah1Test.silahSaldiriHasari;
        seciliSilahSaldiriMenzili = silah1Test.silahSaldiriMenzili;
        seciliSilahKarakterAnimator = silah1Test.karakterAnimator;

        seciliSilahTuru2 = silah2Test.silahTuru;
        seciliSilahAdi2 = silah2Test.silahAdi;
        seciliSilahSaldiriHasari2 = silah2Test.silahSaldiriHasari;
        seciliSilahSaldiriMenzili2 = silah2Test.silahSaldiriMenzili;
        seciliSilahKarakterAnimator2 = silah2Test.karakterAnimator;


        seciliSilah1Image.sprite = silah1Test.seciliSilah.silahIcon;
        seciliSilah2Image.sprite = silah2Test.seciliSilah.silahIcon;
    }
}