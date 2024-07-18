using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aniAgaciAcma : MonoBehaviour
{
    bool acti;
    public GameObject aniAgaci,oyunPaneli;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.I))
        {
            acti = !acti;
            if(acti)
            {
                oyunPaneli.SetActive(false);
                aniAgaci.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                oyunPaneli.SetActive(true);
                aniAgaci.SetActive(false);

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
