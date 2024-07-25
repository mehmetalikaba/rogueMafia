using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class killSayaci : MonoBehaviour
{
    public TextMeshProUGUI oldurmeSayisiText;
    public int oldurmeSayisi;
    public void yazdir()
    {
        oldurmeSayisiText.text=oldurmeSayisi.ToString();
    }
}
