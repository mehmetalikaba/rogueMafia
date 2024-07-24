using UnityEngine;
using TMPro;

public class hasarRaporu : MonoBehaviour
{
    oyuncuSaldiriTest oyuncuSaldiriTest;
    public TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        text.text = oyuncuSaldiriTest.sonHasar.ToString("F0");
    }
}
