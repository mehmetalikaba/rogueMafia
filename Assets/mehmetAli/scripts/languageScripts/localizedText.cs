using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string key;

    private Text text;
    private LocalizationManager localizationManager;

    private void Start()
    {
        localizationManager = FindObjectOfType<LocalizationManager>();
        text = GetComponent<Text>();

        LocalizationManager.dilDegisti += DilDegistiHandler;

        text.text = localizationManager.GetLocalizedValue(key);
    }

    private void DilDegistiHandler()
    {
        text.text = localizationManager.GetLocalizedValue(key);
    }

    private void OnDestroy()
    {
        LocalizationManager.dilDegisti -= DilDegistiHandler;
    }
}
