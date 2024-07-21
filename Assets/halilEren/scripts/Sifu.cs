using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sifu : MonoBehaviour
{
    bool acti;
    public Npc sifuNpc;
    public GameObject aniAgaciPanel, oyunPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sifuNpc.yakin)
        {
            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")))
            {
                acti = !acti;
                if (acti)
                {
                    oyunPanel.SetActive(false);
                    aniAgaciPanel.SetActive(true);

                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    oyunPanel.SetActive(true);
                    aniAgaciPanel.SetActive(false);

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }

        }
    }
}
