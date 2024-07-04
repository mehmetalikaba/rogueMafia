using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    [Header("Important string")]
    private const string FILENAME_PREFIX = "text_";
    private const string FILE_EXTENSION = ".json";
    private string FULL_NAME_TEXT_FILE;
    private string URL = "";
    private string FULL_PATH_TEXT_FILE;
    private string LANGUAGE_CHOOSE = "EN";
    private string LOADED_JSON_TEXT = "";

    [Header("Important bool")]
    private bool _isReady = false;
    private bool _isFileFound = false;
    private bool _isTryChangeLangRunTime = false;

    [Header("Json Variable")]
    private Dictionary<string, string> _localizedDictionary;
    private LocalizationData _loadedData;

    #region Instance Function
    private static LocalizationManager LocalizationManagerInstance;

    public static LocalizationManager Instance
    {
        get
        {
            if(LocalizationManagerInstance == null)
            {
                LocalizationManagerInstance = FindObjectOfType(typeof(LocalizationManager)) as LocalizationManager;
            }
            return LocalizationManagerInstance;
        }
    }
    #endregion Instance Function



    void Start()
    {
        LANGUAGE_CHOOSE = LocaleHelper.dilSecimiGetir();
        FULL_NAME_TEXT_FILE = FILENAME_PREFIX + LANGUAGE_CHOOSE.ToLower() + FILE_EXTENSION;

        FULL_PATH_TEXT_FILE = Path.Combine(Application.streamingAssetsPath, FULL_NAME_TEXT_FILE);

        Debug.Log(FULL_PATH_TEXT_FILE);

    }
}