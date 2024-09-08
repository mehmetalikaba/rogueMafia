using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kontrolcü : MonoBehaviour
{
    oyuncuHareket hareket;
    oyuncuSaldiriTest saldiri;
    // Start is called before the first frame update
    void Start()
    {
        hareket=FindObjectOfType<oyuncuHareket>();
        saldiri=FindObjectOfType<oyuncuSaldiriTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            hareket.Ziplama();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            hareket.Atilma();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            saldiri.SolKlikSaldiri();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            saldiri.SagKlikSaldiri();
        }
    }
}
