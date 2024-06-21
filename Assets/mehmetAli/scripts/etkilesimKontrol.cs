using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class etkilesimKontrol : MonoBehaviour
{

    public bool baslangicBar, sehirBolumu, araBase, isletmeBolumu, garajBase, tershaneBolumu, gemiBase, adaBolumu, bahceBolumu, malikaneBolumu, ustaShifu, panelAcik;

    public TextMeshProUGUI textMeshProUGUI;
    public Image image;
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
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "oyuncu")
        {
            ustaShifu = false;
        }
    }




}
