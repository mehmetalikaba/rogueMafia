using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class etkilesimKontrol : MonoBehaviour
{

    public bool ustaShifu, panelAcik;

    public GameObject isik;

    public GameObject shifuPanel;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad9) && ustaShifu)
        {
            if (panelAcik)
            {
                panelAcik = false;
                shifuPanel.SetActive(false);
            }
            else if (!panelAcik)
            {
                panelAcik = true;
                shifuPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "oyuncu")
        {
            ustaShifu = true;
            isik.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "oyuncu")
        {
            ustaShifu = false;
            isik.SetActive(false);
        }
    }




}
