using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class etkilesimKontrol : MonoBehaviour
{

    public bool buObjeUstaShifu, buObjeAlfred, oyuncuYakin, ustaShifuPanelAcikMi, alfredPanelAcikMi;

    public GameObject shifuPanel, alfredPanel;

    public Light2D light2D;

    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && oyuncuYakin)
        {
            if (buObjeUstaShifu)
            {
                if (!ustaShifuPanelAcikMi)
                {
                    oyunDurdur();
                    ustaShifuPanelAcikMi = true;
                    shifuPanel.SetActive(true);
                }
                else
                {
                    oyunDevamEt();
                    ustaShifuPanelAcikMi = false;
                    shifuPanel.SetActive(false);
                }
            }
            else if (buObjeAlfred)
            {
                if (!alfredPanelAcikMi)
                {
                    oyunDurdur();
                    alfredPanelAcikMi = true;
                    alfredPanel.SetActive(true);
                }
                else
                {
                    oyunDevamEt();
                    alfredPanelAcikMi = false;
                    alfredPanel.SetActive(false);
                }
            }
        }
    }

    public void oyunDurdur()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    public void oyunDevamEt()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "oyuncu")
        {
            oyuncuYakin = true;
            light2D.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "oyuncu")
        {
            oyuncuYakin = false;
            light2D.enabled = false;
        }
    }




}
