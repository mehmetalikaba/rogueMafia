using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    private Dictionary<string, string> localizeMetinler = new Dictionary<string, string>();

    public TextAsset trJson, enJson, jpJson;
    public string seciliDil;
    private string kayitliSeciliDil;

    public delegate void DilDegisti();
    public static event DilDegisti dilDegisti;

    public TMP_Dropdown dropdown;

    public bool menude;

    private void Awake()
    {
        kayitliSeciliDil = PlayerPrefs.GetString("secilenDil");

        if (string.IsNullOrEmpty(kayitliSeciliDil))
        {
            seciliDil = "EN";
            PlayerPrefs.SetString("secilenDil", seciliDil);
        }
        else
            seciliDil = kayitliSeciliDil;

        YeniDilYukle(seciliDil);

        if (menude)
        {
            dropdown.value = GetDropdownIndex(seciliDil);
            dropdown.onValueChanged.AddListener(delegate
            {
                DilSecimDegisti(dropdown.value);
            });
        }
    }

    public void YeniDilYukle(string dilKodu)
    {
        seciliDil = dilKodu;
        PlayerPrefs.SetString("secilenDil", seciliDil);

        localizeMetinler.Clear();
        TextAsset dilDosyasi = GetDilDosyasi(seciliDil);
        if (dilDosyasi != null)
        {
            LocalizationData data = JsonUtility.FromJson<LocalizationData>(dilDosyasi.text);
            foreach (LocalizationItem item in data.items)
            {
                localizeMetinler[item.key] = item.value;
            }

            if (dilDegisti != null)
                dilDegisti();
        }
    }

    private TextAsset GetDilDosyasi(string dilKodu)
    {
        switch (dilKodu)
        {
            case "TR":
                return trJson;
            case "EN":
                return enJson;
            case "JP":
                return jpJson;
            default:
                return null;
        }
    }

    public string GetLocalizedValue(string key)
    {
        if (localizeMetinler.ContainsKey(key))
        {
            return localizeMetinler[key];
        }
        else
        {
            Debug.LogWarning($"Key '{key}' not found in localization dictionary.");
            return "-----";
        }
    }

    private int GetDropdownIndex(string dilKodu)
    {
        switch (dilKodu)
        {
            case "TR":
                return 1;
            case "JP":
                return 2;
            default:
                return 0;
        }
    }

    private void DilSecimDegisti(int index)
    {
        switch (index)
        {
            case 0:
                YeniDilYukle("EN");
                break;
            case 1:
                YeniDilYukle("TR");
                break;
            case 2:
                YeniDilYukle("JP");
                break;
        }
    }

    [System.Serializable]
    public class LocalizationData
    {
        public LocalizationItem[] items;
    }

    [System.Serializable]
    public class LocalizationItem
    {
        public string key;
        public string value;
    }
}
