using UnityEngine;
using TMPro;

public class hasarRaporu : MonoBehaviour
{
    oyuncuSaldiriTest oyuncuSaldiriTest;
    public TextMeshProUGUI text;
    public float alinanHasar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(gameObject.name);
        oyuncuSaldiriTest = FindObjectOfType<oyuncuSaldiriTest>();
        text.text = alinanHasar.ToString("F0");
    }
}
