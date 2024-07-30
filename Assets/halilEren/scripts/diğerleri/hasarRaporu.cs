using UnityEngine;
using TMPro;

public class hasarRaporu : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float alinanHasar;

    void Start()
    {
        text.text = alinanHasar.ToString("F0");
    }
}
