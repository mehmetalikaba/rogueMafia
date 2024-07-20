using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aniAgaciAcma : MonoBehaviour
{
    bool acti;
    public GameObject aniAgaci, oyunPaneli;
    public yetenekKontrol yetenekKontrol;
    // Start is called before the first frame update
    void Start()
    {
        aniAgaci.SetActive(false);
        yetenekKontrol = FindObjectOfType<yetenekKontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            yetenekKontrol.skilleriUygulama();
            acti = !acti;
            if (acti)
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
