using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tusDizilimiGetirTest : MonoBehaviour
{
    public static tusDizilimiGetirTest instance;
    public tusDizilimiTest tusDizilimi;

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
                return;
            }
        }
    }
}