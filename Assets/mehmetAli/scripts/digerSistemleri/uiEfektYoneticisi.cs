using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiEfektYoneticisi : MonoBehaviour
{
    public Image efektUygulanacakObje;
    public string objeninCalismaTusu;
    public bool basildi;

    void Start()
    {
        efektUygulanacakObje = GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir(objeninCalismaTusu)) && !basildi)
        {
            StartCoroutine(butonaBasmaEfekti());
        }
    }
    IEnumerator butonaBasmaEfekti()
    {
        basildi = true;
        Vector3 originalScale = efektUygulanacakObje.transform.localScale;
        Vector3 clickedScale = originalScale * 0.8f;
        float elapsedTime = 0f;
        float duration = 0.1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            efektUygulanacakObje.transform.localScale = Vector3.Lerp(originalScale, clickedScale, elapsedTime / duration);
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            efektUygulanacakObje.transform.localScale = Vector3.Lerp(clickedScale, originalScale, elapsedTime / duration);
            yield return null;
        }
        basildi = false;
    }
}
