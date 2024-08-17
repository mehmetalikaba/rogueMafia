using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class localizedText : MonoBehaviour
{
    public string key;

    private Text text;
    private TextMeshProUGUI textMeshPro;
    private LocalizationManager localizationManager;
    private string previousValue;

    private void Start()
    {
        localizationManager = FindObjectOfType<LocalizationManager>();
        if (localizationManager == null)
        {
            Debug.LogError("LocalizationManager not found in the scene.");
            return;
        }

        text = GetComponent<Text>();
        if (text == null)
            textMeshPro = GetComponent<TextMeshProUGUI>();

        UpdateText();
    }

    private void Update()
    {
        if (key == "")
        {
            text.text = "";
            previousValue = "";
        }
        else
        {
            string localizedValue = localizationManager.GetLocalizedValue(key);

            if (localizedValue != previousValue)
            {
                previousValue = localizedValue;
                if (text != null)
                    text.text = localizedValue;
                else if (textMeshPro != null)
                    textMeshPro.text = localizedValue;
            }
        }
    }

    public void SetKey(string newKey)
    {
        key = newKey;
        UpdateText();
    }

    private void UpdateText()
    {
        if (string.IsNullOrEmpty(key) || localizationManager == null)
            return;

        string localizedValue = localizationManager.GetLocalizedValue(key);
        previousValue = localizedValue;

        if (text != null)
            text.text = localizedValue;
        else if (textMeshPro != null)
            textMeshPro.text = localizedValue;
    }
}
