using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tusDizilimiGetirTest : MonoBehaviour
{
    public static tusDizilimiGetirTest instance;
    public tusDizilimiTest tusDizilimi;

    public Text[] texts;

    private void Awake()
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

    private void Start()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = tusDizilimi.bindings[i+2].key.ToString();
            if (texts[i].text=="Mouse0")
            {
                texts[i].text = "Mouse Sol Tuş";
            }
            if (texts[i].text == "Mouse1")
            {
                texts[i].text = "Mouse Sag Tuş";
            }
            if (texts[i].text == "Mouse2")
            {
                texts[i].text = "Mouse Orta Tuş";
            }
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
                    texts[i].text = tusDizilimi.bindings[i + 2].key.ToString();
                    if (texts[i].text == "Mouse0")
                    {
                        texts[i].text = "Mouse Sol Tuş";
                    }
                    if (texts[i].text == "Mouse1")
                    {
                        texts[i].text = "Mouse Sag Tuş";
                    }
                    if (texts[i].text == "Mouse2")
                    {
                        texts[i].text = "Mouse Orta Tuş";
                    }
                }
                return;
            }
        }
    }
}