using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tusDizilimleri : MonoBehaviour
{
    public static tusDizilimleri instance;
    public tusDizilimiTest tusDizilimi;
    public LocalizationManager localizationManager;
    public string solTus, sagTus, ortaTus;

    public Text[] texts;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //tusMetinleriGetir();
    }


    public void tusMetinleriGetir()
    {
        solTus = localizationManager.GetLocalizedValue("sol_tus");
        sagTus = localizationManager.GetLocalizedValue("sag_tus");
        ortaTus = localizationManager.GetLocalizedValue("orta_tus");

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = tusDizilimi.bindings[i].key.ToString();
            if (texts[i].text == "Mouse0")
                texts[i].text = solTus;
            if (texts[i].text == "Mouse1")
                texts[i].text = sagTus;
            if (texts[i].text == "Mouse2")
                texts[i].text = ortaTus;
        }
    }

    public KeyCode tusIsleviGetir(string action)
    {
        return tusDizilimi.tusIsleviGetir(action);
    }

    public void SetKeyForAction(string action, KeyCode newKey)
    {
        foreach (var binding in tusDizilimi.bindings)
        {
            if (binding.action == action)
            {
                binding.key = newKey;
                for (int i = 0; i < texts.Length; i++)
                {
                    texts[i].text = tusDizilimi.bindings[i].key.ToString();
                    if (texts[i].text == "Mouse0")
                        texts[i].text = solTus;
                    if (texts[i].text == "Mouse1")
                        texts[i].text = sagTus;
                    if (texts[i].text == "Mouse2")
                        texts[i].text = ortaTus;
                }
                return;
            }
        }
    }
}