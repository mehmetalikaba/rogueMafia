using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class silahUltileri : MonoBehaviour
{

    oyuncuSaldiriTest oyuncuSaldiriTest;

    public bool ultiAcik;

    public float silah1Ulti, silah2Ulti;

    public Image silah1UltiBar, silah2UltiBar;

    public TextMeshProUGUI ultiHazirText;

    public GameObject ultiHazirTextObject;

    void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
    }

    void Update()
    {
        silah1UltiBar.fillAmount = silah1Ulti / 100f;
        silah2UltiBar.fillAmount = silah2Ulti / 100f;

        if (silah1Ulti >= 100 || silah2Ulti >= 100)
        {
            ultiHazirTextObject.SetActive(true);
        }
        if (ultiAcik)
        {
            ultiHazirText.text = "Ulti\nHazir";
        }

        if (Input.GetKeyDown(KeyCode.X) && (silah1Ulti >= 100 || silah2Ulti >= 100))
        {
            ultiAcik = true;
        }
        if (Input.GetMouseButton(0) && ultiAcik)
        {
            silah1Ulti = 0f;

            ultiSaldirilari();
        }
        if (Input.GetMouseButton(1) && ultiAcik)
        {
            silah2Ulti = 0f;

            ultiSaldirilari();
        }
    }

    public void ultiSaldirilari()
    {
        ultiAcik = false;
        ultiHazirTextObject.SetActive(false);
        ultiHazirText.text = "Ulti\nX";

        oyuncuSaldiriTest.ultiSaldiri();
    }
}
