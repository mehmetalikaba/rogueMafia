using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aniAgaciAcma : MonoBehaviour
{
    public GameObject aniAgaci,oyunPaneli;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            oyunPaneli.SetActive(false);
            aniAgaci.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
