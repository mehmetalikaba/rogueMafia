using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class aldigiObjeYazsin : MonoBehaviour
{
    private void Start()
    {

    }

    /*
    public GameObject[] textObjeleri;
    public string[] keys;
    public string[] simdikiKey;
    private RectTransform[] rectTransform; // RectTransform dizisi
    public localizedText[] localizedTexts;
    public Text[] texts;
    public scriptKontrol scriptKontrol;

    void Start()
    {
        scriptKontrol = FindObjectOfType<scriptKontrol>();

        simdikiKey = new string[5];
        rectTransform = new RectTransform[textObjeleri.Length]; // RectTransform dizisini baþlat

        simdikiKey[0] = scriptKontrol.silah1OzellikleriniGetir.aciklamaKeyi;
        simdikiKey[1] = scriptKontrol.silah2OzellikleriniGetir.aciklamaKeyi;
        simdikiKey[2] = scriptKontrol.ozelGuc1KullanmaScripti.ozelGucAciklamaKeyi;
        simdikiKey[3] = scriptKontrol.ozelGuc2KullanmaScripti.ozelGucAciklamaKeyi;
        simdikiKey[4] = scriptKontrol.toplanabilirKullanmaScripti.toplanabilirAciklamaKeyi;

        for (int i = 0; i < textObjeleri.Length; i++)
        {
            rectTransform[i] = textObjeleri[i].GetComponent<RectTransform>();
        }
        for (int i = 0; i < textObjeleri.Length; i++)
        {
            localizedTexts[i] = textObjeleri[i].GetComponent<localizedText>();
        }
    }

    void Update()
    {
        for (int i = 0; i < textObjeleri.Length; i++)
        {
            if (textObjeleri[i].GetComponent<localizedText>() != null)
            {
                string currentKey = textObjeleri[i].GetComponent<localizedText>().key;

                if (currentKey != simdikiKey[i])
                {
                    Debug.Log($"{simdikiKey[i]} deðiþti");
                    simdikiKey[i] = currentKey;
                    StartCoroutine(objeYukariKayacak(rectTransform[i], 5, 5));
                }
            }
        }
    }

    IEnumerator objeYukariKayacak(RectTransform rectTransform, float distance, float duration)
    {
        Vector2 originalPosition = rectTransform.anchoredPosition;
        Vector2 targetPosition = originalPosition + new Vector2(0, distance);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
    }*/
}