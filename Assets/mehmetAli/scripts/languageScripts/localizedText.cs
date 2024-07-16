using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class localizedText : MonoBehaviour
{
    public string key;

    private Text text;
    private TextMeshProUGUI textMeshPro;
    private LocalizationManager localizationManager;

    private void Start()
    {
        if (key != "")
            DilDegistiHandler();
        else
            Debug.Log("bos");
    }

    public void DilDegistiHandler()
    {
        localizationManager = FindObjectOfType<LocalizationManager>();

        text = GetComponent<Text>();
        if (text == null)
            textMeshPro = GetComponent<TextMeshProUGUI>();

        LocalizationManager.dilDegisti += DilDegistiHandler;
        text.text = localizationManager.GetLocalizedValue(key);
    }

    private void OnDestroy()
    {
        LocalizationManager.dilDegisti -= DilDegistiHandler;
    }
}
