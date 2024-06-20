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
    public float seciliSilahSaldiriHizi;
    public Animator seciliSilahKarakterAnimator;

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
        silahSpriteGetir();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            silah1Secili = true;
            silah2Secili = false;

            seciliSilahTuru = silah1Test.silahTuru;
            seciliSilahAdi = silah1Test.silahAdi;
            seciliSilahSaldiriHasari = silah1Test.silahSaldiriHasari;
            seciliSilahSaldiriHizi = silah1Test.silahSaldiriHizi;
            seciliSilahKarakterAnimator = silah1Test.karakterAnimator;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            silah1Secili = false;
            silah2Secili = true;

            seciliSilahTuru = silah2Test.silahTuru;
            seciliSilahAdi = silah2Test.silahAdi;
            seciliSilahSaldiriHasari = silah2Test.silahSaldiriHasari;
            seciliSilahSaldiriHizi = silah2Test.silahSaldiriHizi;
            seciliSilahKarakterAnimator = silah2Test.karakterAnimator;
        }

    }

    public void silahSpriteGetir()
    {
        seciliSilah1Image.sprite = silah1Test.selectedWeapon.silahIcon;
        seciliSilah2Image.sprite = silah2Test.selectedWeapon.silahIcon;
    }
}
