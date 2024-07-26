using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class aldigiObjeYazsin : MonoBehaviour
{
    public GameObject aldigiObjeText;
    public string key;

    private RectTransform rectTransform;

    public Text[] texts = new Text[5]; // 5 text objesi olacak
    private int currentTextIndex = 0;

    void Start()
    {
        rectTransform = aldigiObjeText.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (aldigiObjeText.GetComponent<localizedText>().key != key)
        {
            aldigiObjeText.GetComponent<localizedText>().key = key;
            StartCoroutine(HandleTextMovement());
        }
    }

    IEnumerator HandleTextMovement()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            StartCoroutine(MoveTextSmoothly(texts[i].rectTransform, 50f, 1f));
        }

        // Bekle ki textler yukarý doðru kaydýrýlsýn
        yield return new WaitForSeconds(1f);

        // En üstteki text objesini yok et
        Destroy(texts[currentTextIndex].gameObject);

        // Yeni text objesini en altta oluþtur
        GameObject newTextObject = Instantiate(aldigiObjeText, texts[currentTextIndex].transform.parent);
        Text newText = newTextObject.GetComponent<Text>();
        RectTransform newTextRectTransform = newTextObject.GetComponent<RectTransform>();
        newTextRectTransform.anchoredPosition = new Vector2(newTextRectTransform.anchoredPosition.x, newTextRectTransform.anchoredPosition.y - 200); // Alt tarafa yerleþtir
        texts[currentTextIndex] = newText;

        currentTextIndex = (currentTextIndex + 1) % texts.Length;
    }

    IEnumerator MoveTextSmoothly(RectTransform rectTransform, float distance, float duration)
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
    }
}
