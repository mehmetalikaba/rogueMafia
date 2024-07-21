using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sifu : MonoBehaviour
{
    public Npc sifuNpc;
    public GameObject aniAgaciPanel, oyunPanel;

    void Update()
    {
        if (sifuNpc.yakin)
        {
            if (Input.GetKeyDown(tusDizilimleri.instance.tusIsleviGetir("fTusu")))
            {
                if (!aniAgaciPanel.activeSelf)
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
