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
    }

    public void DilDegistiHandler()
    {
        if (this == null)
        {
            Debug.LogWarning("DilDegistiHandler called on a destroyed object.");
            return;
        }

        localizationManager = FindObjectOfType<LocalizationManager>();

        if (localizationManager == null)
        {
            Debug.LogWarning("LocalizationManager not found.");
            return;
        }
        text = GetComponent<Text>();
        if (text == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        if (text != null)
        {
            text.text = localizationManager.GetLocalizedValue(key);
        }
        else if (textMeshPro != null)
        {
            textMeshPro.text = localizationManager.GetLocalizedValue(key);
        }
        else
        {
            Debug.LogWarning($"No Text or TextMeshProUGUI component found on {gameObject.name}.");
        }


        LocalizationManager.dilDegisti += DilDegistiHandler;
        text.text = localizationManager.GetLocalizedValue(key);
    }

    private void OnDestroy()
    {
        LocalizationManager.dilDegisti -= DilDegistiHandler;
    }
}
