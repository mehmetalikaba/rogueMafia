using UnityEngine;
using UnityEngine.UI;

public class rastgeleSilah : MonoBehaviour
{

    public silahSecimi silahSecimi;

    public silahTest silahTest;

    public Image silah1Image, silah2Image, silah3Image;

    public Button silah1Buton, silah2Buton, silah3Buton;

    public silahlarTest[] butunSilahlarTest;

    public silahlarTest[] secilenSilahlarTest;

    public string[] seciliSilah;


    private void Awake()
    {
        rastgeleSilahlarAta();
    }

    void Start()
    {
        silahTest = FindAnyObjectByType<silahTest>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            silahIconlariGuncelle();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            rastgeleSilahlarAta();
        }
    }

    void rastgeleSilahlarAta()
    {
        System.Collections.Generic.List<silahlarTest> silahListesiTest = new System.Collections.Generic.List<silahlarTest>(butunSilahlarTest);
        secilenSilahlarTest = new silahlarTest[3];

        for (int i = 0; i < 3; i++)
        {
            int rastgeleIndexTest = Random.Range(0, silahListesiTest.Count);
            secilenSilahlarTest[i] = silahListesiTest[rastgeleIndexTest];
            silahListesiTest.RemoveAt(rastgeleIndexTest);
        }
    }

    void silahIconlariGuncelle()
    {
        if (secilenSilahlarTest != null && secilenSilahlarTest.Length == 3)
        {
            silah1Image.sprite = secilenSilahlarTest[0].silahIcon;
            silah2Image.sprite = secilenSilahlarTest[1].silahIcon;
            silah3Image.sprite = secilenSilahlarTest[2].silahIcon;
        }
    }

    public void silah1ButonunaBasti()
    {
        seciliSilah[0] = secilenSilahlarTest[0].silahAdi;
        Debug.Log(secilenSilahlarTest[0].silahAdi);
    }
    public void silah2ButonunaBasti()
    {
        seciliSilah[1] = secilenSilahlarTest[1].silahAdi;
        Debug.Log(secilenSilahlarTest[1].silahAdi);
    }
    public void silah3ButonunaBasti()
    {
        seciliSilah[2] = secilenSilahlarTest[2].silahAdi;
        Debug.Log(secilenSilahlarTest[2].silahAdi);
    }
}
