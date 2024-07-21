using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "tusDizilimi", menuName = "Scriptable Objects/KeyBindings")]
public class tusDizilimiTest : ScriptableObject
{
    [System.Serializable]
    public class KeyBinding
    {
        public string action;
        public KeyCode key;
    }

    public List<KeyBinding> bindings = new List<KeyBinding>();

    public KeyCode tusIsleviGetir(string action)
    {
        foreach (var binding in bindings)
        {
            if (binding.action == action)
            {
                return binding.key;
            }
        }
        return KeyCode.None;
    }
}