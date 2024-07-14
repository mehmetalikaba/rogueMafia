using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class silahUltileri : MonoBehaviour
{
    oyuncuSaldiriTest oyuncuSaldiriTest;

    public bool silah1UltiAcik, silah2UltiAcik;

    public float silah1Ulti, silah2Ulti;

    public Image silah1UltiBar, silah2UltiBar;

    public TextMeshProUGUI silah1UltiHazirText, silah2UltiHazirText;

    public GameObject silah1UltiHazirTextObject, silah2UltiHazirTextObject;

    void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
    }

    void Update()
    {
        silah1UltiBar.fillAmount = silah1Ulti / 100f;
        silah2UltiBar.fillAmount = silah2Ulti / 100f;

        if (silah1Ulti >= 100)
            silah1UltiHazirTextObject.SetActive(true);
        if (silah2Ulti >= 100)
            silah2UltiHazirTextObject.SetActive(true);

        if (silah1UltiAcik)
            silah1UltiHazirText.text = "Ulti\nHazir";
        if (silah2UltiAcik)
            silah2UltiHazirText.text = "Ulti\nHazir";

        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("xTusu")))
        {
            if (silah1Ulti >= 100)
                silah1UltiAcik = true;
            else if (silah2Ulti >= 100)
                silah2UltiAcik = true;
        }
    }
    public void silah1UltiSaldiri()
    {
        silah1Ulti = 0f;
        silah1UltiAcik = false;
        silah1UltiHazirTextObject.SetActive(false);
        silah1UltiHazirText.text = "Ulti\nX";
        oyuncuSaldiriTest.silah1UltiSaldiri();
    }
    public void silah2UltiSaldiri()
    {
        silah2Ulti = 0f;
        silah2UltiAcik = false;
        silah2UltiHazirTextObject.SetActive(false);
        silah2UltiHazirText.text = "Ulti\nX";
        oyuncuSaldiriTest.silah2UltiSaldiri();
    }
}
