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
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            oldurmeSayisi++;
            oldurmeSayisiText.text = oldurmeSayisi.ToString();

        }
    }
}
