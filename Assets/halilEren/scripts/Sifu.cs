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
                    aniAgaciAc();
                else
                    aniAgaciKapat();
            }
        }
    }

    public void aniAgaciAc()
    {
        oyunPanel.SetActive(false);
        aniAgaciPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void aniAgaciKapat()
    {
        oyunPanel.SetActive(true);
        aniAgaciPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
